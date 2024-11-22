using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Jongmin;
using UnityEditor;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    private Dictionary<ObjectData, string[]> m_talk_data;
    private Dictionary<ObjectData, Sprite> m_portrait_data;
    private Taekyung.QuestData m_quest_data;
    //private Dictionary<ObjectData, string[]> m_quest_data;

    public TalkUIManager m_talk_ui_manager;
    public QuestManager m_quest_manager;
    public PlayerData m_player_data;

    public Sprite[] m_portrait_arr;

    public GameObject m_scan_object;
    public bool m_is_action;

    private string m_save_path;

    private void Start()
    {
        m_save_path = Application.persistentDataPath + "/Data";
 
        m_portrait_data = new Dictionary<ObjectData, Sprite>();

        GeneratePortrait();

        BringTalkLineDataFromJson();
        //BringQuestLineDataFromJson();
    } 
    
    // 대화 중 초상화 UI를 생성하기 위한 메소드
    public void GeneratePortrait()
    {
        m_portrait_data.Add(new ObjectData(1000 + 0, true), m_portrait_arr[0]);
    }

    // JSON 파일에서 NPC 정보와 대사를 불러오는 메소드
    public void BringTalkLineDataFromJson()
    {
        string json_path = m_save_path + "/TalkData.json";

        if(!System.IO.File.Exists(json_path))
        {
            Debug.Log($"{m_save_path}에 일치하는 TalkData.json이 없습니다.");
            return;
        }

        string json_string = System.IO.File.ReadAllText(json_path);
        TalkDataWrapper talk_data_wrapper = JsonUtility.FromJson<TalkDataWrapper>(json_string);

        if(talk_data_wrapper == null || talk_data_wrapper.m_talk_datas == null)
        {
            Debug.Log($"{m_save_path}/TalkData.json에서 NPC 정보와 대사를 불러오는 데 실패하였습니다.");
            return;
        }
        else
        {
            Debug.Log($"{m_save_path}/TalkData.json에서 NPC 정보와 대사를 불러오는 데 성공하였습니다.");
        }

        m_talk_data = new Dictionary<ObjectData, string[]>();
        foreach(var data in talk_data_wrapper.m_talk_datas)
        {
            if(data.m_object_data == null)
                continue;

            m_talk_data[data.m_object_data] = data.m_talk_data;
        }
    }
    /*
    // JSON 파일에서 NPC 정보와 퀘스트 대사를 불러오는 메소드
    public void BringQuestLineDataFromJson() 
    {
        string json_path = m_save_path + "/QuestData.json";

        if(!System.IO.File.Exists(json_path))
        {
            Debug.Log($"{m_save_path}에 일치하는 QuestData.json이 없습니다.");
            return;
        }

        string json_string = System.IO.File.ReadAllText(json_path);
        QuestDataWrapper quest_data_wrapper = JsonUtility.FromJson<QuestDataWrapper>(json_string);

        if(quest_data_wrapper == null || quest_data_wrapper.m_quest_datas == null)
        {
            Debug.Log($"{m_save_path}/QuestData.json에서 퀘스트 정보를 불러오는 데 실패하였습니다.");
            return;
        }
        else
        {
            Debug.Log($"{m_save_path}/QuestData.json에서 퀘스트 정보를 불러오는 데 성공하였습니다.");
        }

        m_talk_data = new Dictionary<ObjectData, string[]>();
        foreach(var data in quest_data_wrapper.m_quest_datas)
        {
            if(data.m_object_data == null)
                continue;

            m_talk_data[data.m_object_data] = data.m_talk_data;
        }
    }
    */
    // id와 일치하는 NPC의 대사를 리턴하는 메소드
    public string GetTalkData(int id, int talk_idx) 
    {
        foreach (var key in m_talk_data.Keys)
        {
            if(key.m_id == id)
            {
                if (talk_idx >= m_talk_data[key].Length)
                {
                    return null;
                }

                return m_talk_data[key][talk_idx];
            }
        }

        return null;
    }

    // id와 일치하는 NPC의 스프라이트를 리턴하는 메소드
    public Sprite GetPortrait(int id, int portrait_idx) 
    {
        foreach (var key in m_portrait_data.Keys)
        {
            if (key.m_id == id + portrait_idx)
            {
                return m_portrait_data[key];
            }
        }
        return null;
    }

    // 상호작용 메소드
    public void InteractionWithObject(GameObject scan_object)
    {
        if(m_is_action)
        {
            m_is_action = false;
        }
        else
        {
            m_is_action = true;
            m_scan_object = scan_object;

            ObjectData object_data = m_scan_object.GetComponent<ObjectData>();
            m_player_data.m_talk_id = object_data.m_id;
            if(m_player_data.m_quest_id == object_data.m_id)
            {
                m_quest_data = m_quest_manager.GetQuestData(m_player_data.m_quest_id); // 퀘스트 제목 및 퀘스트 npc 정보 받아올 수 있음
                Debug.Log($"{m_quest_data.m_quest_name}");
                Debug.Log($"{m_quest_data.m_npc_id}");
                Talk(10000 + m_player_data.m_quest_id, object_data.m_is_npc);
            }
            else
            {
                Talk(m_player_data.m_talk_id, object_data.m_is_npc);
            }
        }

        m_talk_ui_manager.SetTalkUIActive(m_is_action);
    }

    // 대화를 진행하는 메소드
    private void Talk(int id, bool is_npc)
    {
        // Set Talk Data
        string talk_data = GetTalkData(id, m_player_data.m_talk_idx);

        // End Talk 
        if(talk_data == null)
        {
            m_is_action = false;
            if (m_quest_manager.CheckQuesk(id > 10000 ? id - 10000 : id))
            {
                m_quest_manager.ControlObject(id > 10000 ? id - 10000 : id);
                m_player_data.m_quest_id++;
            }

            m_player_data.m_talk_idx = 0;

            return;
        }
        // : 이후 숫자에 따른 초상화 선택 및 대사 선택
        string[] split_data = talk_data.Split(':');
        string text = split_data[0];
        int portrait_index = split_data.Length > 1 ? int.Parse(split_data[1]) : 0;

        // 플레이어의 대사 차례인지 확인
        bool is_player;
        if(split_data.Length > 2)
        {
            is_player = true;
        }
        else
        {
            is_player= false;
        }

        // 초상화 가져오기
        Sprite portrait = (is_npc && portrait_index >= 0)
            ? GetPortrait(id > 10000 ? id - 10000 : id, portrait_index)
            : null;


        // ui 변경
        m_talk_ui_manager.UpdateTalkUI(text, portrait, is_npc, is_player);

        m_is_action = true;
        m_player_data.m_talk_idx++;
    }
}
