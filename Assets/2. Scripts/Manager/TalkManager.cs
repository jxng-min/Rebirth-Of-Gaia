using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    private Dictionary<ObjectData, string[]> m_talk_data;
    private Dictionary<ObjectData, Sprite> m_portrait_data;
    private Dictionary<ObjectData, string[]> m_quest_data;

    public TalkUIManager m_talk_ui_manager;

    public Sprite[] m_portrait_arr;
    public string m_json_talk_data;
    public string m_json_quest_data;

    public GameObject m_scan_object;
    public bool m_is_action;
    public int m_talk_idx;

    private void Start()
    {
        m_talk_data = new Dictionary<ObjectData, string[]>();
        m_portrait_data = new Dictionary<ObjectData, Sprite>();
        m_quest_data = new Dictionary<ObjectData, string[]>();

        GeneratePortrait();

        BringQuestLineDataFromJson();
        BringTalkLineDataFromJson();
    } 
    
    // 대화 중 초상화 UI를 생성하기 위한 메소드
    public void GeneratePortrait()
    {
        m_portrait_data.Add(new ObjectData(1000 + 0, true), m_portrait_arr[0]);
    }

    // JSON 파일에서 NPC 정보와 대사를 불러오는 메소드
    public void BringTalkLineDataFromJson()
    {
        m_talk_data = JsonUtility.FromJson<Dictionary<ObjectData, string[]>>(m_json_talk_data);
    }
   
    // JSON 파일에서 NPC 정보와 퀘스트 대사를 불러오는 메소드
    public void BringQuestLineDataFromJson() 
    {
        m_quest_data = JsonUtility.FromJson<Dictionary<ObjectData, string[]>>(m_json_quest_data);
    }

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
            Talk(object_data.m_id, object_data.m_is_npc);
        }

        m_talk_ui_manager.SetTalkUIActive(m_is_action);
    }

    // 대화를 진행하는 메소드
    private void Talk(int id, bool is_npc)
    {
        string talk_data = GetTalkData(id, m_talk_idx);

        if(talk_data == null)
        {
            m_is_action = false;
            m_talk_idx = 0;

            return;
        }

        string[] split_data = talk_data.Split(':');
        string text = split_data[0];
        int portrait_index = split_data.Length > 1 ? int.Parse(split_data[1]) : 0;

        Sprite portrait = (is_npc && portrait_index >= 0)
            ? GetPortrait(id, portrait_index)
            : null;

        bool is_player = (id == 1000);
        m_talk_ui_manager.UpdateTalkUI(text, portrait, is_npc, is_player);

        m_is_action = true;
        m_talk_idx++;
    }
}
