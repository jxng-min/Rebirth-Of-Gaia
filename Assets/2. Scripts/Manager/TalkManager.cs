using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TalkManager:MonoBehaviour
{
    //데이터 관리
    private Dictionary<ObjectData, string[]> m_talk_data;
    private Dictionary<ObjectData, Sprite> m_portrait_data;
    private Dictionary<ObjectData, string[]> m_quest_data;

    public TalkUiManager m_talk_ui_manager;

    public Sprite[] m_portrait_arr;
    public string m_json_talk_data;
    public string m_json_quest_data;

    //상태 관리
    public GameObject m_scan_object;
    public bool m_is_action;
    public int m_talk_idx;

    private void Start()
    {
        //데이터 초기화
        m_talk_data = new Dictionary<ObjectData, string[]>();
        m_portrait_data = new Dictionary<ObjectData, Sprite>();
        m_quest_data = new Dictionary<ObjectData, string[]>();
        // 초상화 집어넣기
        GeneratePortrait();
        // json 데이터 불러오기
        BringQuestLineDataFromJson();
        BringTalkLineDataFromJson();
    } 
    
    // 초상화 arr에서 dic으로 저장
    public void GeneratePortrait()
    {
        m_portrait_data.Add(new ObjectData(1000 + 0, true), m_portrait_arr[0]);
    }
    //jsom 파일에서 대사 불러오기
    public void BringTalkLineDataFromJson()
    {
        m_talk_data = JsonUtility.FromJson<Dictionary<ObjectData, string[]>>(m_json_talk_data);
    }
   
    // json 파일에서 퀘스트 불러오기
    public void BringQuestLineDataFromJson() 
    {
        m_quest_data = JsonUtility.FromJson<Dictionary<ObjectData, string[]>>(m_json_quest_data);
    }

    //설정한 대사 불러오기
    public string GetTalkData(int id, int talk_idx) 
    {
        foreach (var key in m_talk_data.Keys)
        {
            if(key.m_id == id)
            {
                if (talk_idx >= m_talk_data[key].Length)
                    return null;

                return m_talk_data[key][talk_idx];
            }
        }

        return null;
    }

    // 초상화
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

    // 상호작용 여부
    public void Action(GameObject scanObj)
    {
        if(m_is_action)
        {
            m_is_action = false;
        }
        else
        {
            m_is_action = true;
            m_scan_object = scanObj;
            ObjectData object_data = m_scan_object.GetComponent<ObjectData>();
            Talk(object_data.m_id, object_data.m_is_npc);
        }
        m_talk_ui_manager.SetTalkUiActive(m_is_action);
    }

    //대화 여부
    private void Talk(int m_id, bool m_is_npc)
    {
        string talk_data = GetTalkData(m_id, m_talk_idx);

        if(talk_data == null)
        {
            m_is_action = false;
            m_talk_idx = 0;
            return;
        }

        string[] split_data = talk_data.Split(':');
        string text = split_data[0];
        int portrait_index = split_data.Length > 1 ? int.Parse(split_data[1]) : 0;

        bool is_player = m_id == 1000;

        Sprite portrait = m_is_npc && portrait_index >= 0
            ? GetPortrait(m_id, portrait_index)
            : null;

        m_talk_ui_manager.UpdateTalkUi(text, portrait, m_is_npc, is_player);
        m_is_action = true;
        m_talk_idx++;
    }
}
