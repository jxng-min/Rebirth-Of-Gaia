using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<int, QuestData> m_quest_list;
    public string m_json_quest_data;

    void Start()
    {
        m_quest_list = new Dictionary<int, QuestData>();
    }

    public void BringQuestDataFromJson()
    {
        m_quest_list = JsonUtility.FromJson<Dictionary<int, QuestData>>(m_json_quest_data);
    }

    public int GetQuestLineIndex(int npc_id)
    {
        return 0;
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
