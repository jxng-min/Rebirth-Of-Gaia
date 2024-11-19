using Jongmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int m_quest_id; //실행하는 퀘스트 번호
    public int m_quest_action_idx;
    public GameObject[] m_quest_object;
    private Dictionary<int, Taekyung.QuestData> m_quest_list;
    public string m_json_quest_list;
    private string m_save_path;

    void Start()
    {
        m_save_path = Application.persistentDataPath + "/Data";
        m_quest_list = new Dictionary<int, Taekyung.QuestData>();

        BringQuestDataFromJson();
    }

    public void BringQuestDataFromJson()
    {
        string json_path = m_save_path + "/QuestList.json";

        if (!System.IO.File.Exists(json_path))
        {
            Debug.Log($"{m_save_path}에 일치하는 QuestList.json이 없습니다.");
            return;
        }

        string json_string = System.IO.File.ReadAllText(json_path);
        QuestListWrapper quest_data_wrapper = JsonUtility.FromJson<QuestListWrapper>(json_string);

        if (quest_data_wrapper == null || quest_data_wrapper.m_quest_lists == null)
        {
            Debug.Log($"{m_save_path}/QuestList.json에서 퀘스트 정보를 불러오는 데 실패하였습니다.");
            return;
        }
        else
        {
            Debug.Log($"{m_save_path}/QuestList.json에서 퀘스트 정보를 불러오는 데 성공하였습니다.");
        }

        m_quest_list = new Dictionary<int, Taekyung.QuestData>();
        foreach (var data in quest_data_wrapper.m_quest_lists)
        {
            if (data.m_quest_list == null)
                continue;

            m_quest_list[data.m_quest_id] = data.m_quest_list;
        }
    }

    // 해당 퀘스트 제목 및 npc 전달
    public Taekyung.QuestData GetQuestData(int quest_id)
    {
        if (m_quest_list[quest_id] == null)
        {
            Debug.Log($"할당된 퀘스트가 없습니다.");
        }
        else
        {
            Debug.Log($"할당된 퀘스트가 있습니다.");
        }
        return m_quest_list[quest_id];
    }

    public bool CheckQuesk(int id)
    {
        if (id == m_quest_id)
            return true;
        else
            return false;
    }


    // 퀘스트 내용에 따라 오브젝트 활성화/비활성화
    public void ControlObject(int quest_id)
    {
        switch(quest_id)
        {
            case 0:
                m_quest_object[0].SetActive(true);
                break;
        }
    }
    
    private void ControlQuestReward()
    {

    }
}
