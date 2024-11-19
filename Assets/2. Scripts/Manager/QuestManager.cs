using Jongmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int m_quest_id;
    public int m_quest_action_idx;
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

    public int GetQuestLineIndex(int npc_id)
    {
        return m_quest_id;
    }

    public string CheckQuesk(int id)
    {
        return null;
    }

    private void SetNextQuest()
    {

    }
    
    private void ControlQuestReward()
    {

    }
}
