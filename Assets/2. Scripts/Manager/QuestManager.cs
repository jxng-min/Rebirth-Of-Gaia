using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Taekyung;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, QuestData> m_quest_list;
    private string[] m_current_quest_id;

    private string m_save_path;
    private void Start()
    {
        m_save_path = Application.persistentDataPath + "/Data";

        BringQuestDataFromJson();
    }

    public void BringQuestDataFromJson()
    {
        string json_path = m_save_path + "/QuestData.json";

        if (!System.IO.File.Exists(json_path))
        {
            Debug.Log($"{m_save_path}에 일치하는 QuestData.json이 없습니다.");
            return;
        }

        string json_string = System.IO.File.ReadAllText(json_path);
        QuestDataWrapper quest_data_wrapper = JsonUtility.FromJson<QuestDataWrapper>(json_string);

        if (quest_data_wrapper == null || quest_data_wrapper.m_quest_datas == null)
        {
            Debug.Log($"{m_save_path}/QuestData.json에서 퀘스트 정보와 대사를 불러오는 데 실패하였습니다.");
            return;
        }
        else
        {
            Debug.Log($"{m_save_path}/QuestData.json에서 퀘스트 정보와 대사를 불러오는 데 성공하였습니다.");
        }

        m_quest_list = new Dictionary<string, QuestData>();
        foreach (var data in quest_data_wrapper.m_quest_datas)
        {
            if (data.m_quest_name == null)
                continue;
            m_quest_list[data.m_quest_id] = data;
        }
    }

    public bool CheckQuest(string quest_id)
    {
        if (m_quest_list.ContainsKey(quest_id))
        {
            return true;
        }
        else
        {
            Debug.Log($"m_quest_list에 {quest_id}가 없음");
            return false;
        }
    }

    public void StartQuest(string quest_id)
    {

    }
}
