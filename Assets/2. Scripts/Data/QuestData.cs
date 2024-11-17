using UnityEngine;

public class QuestData
{
    string m_quest_name;
    int[] m_npc_id;
    
    QuestData(string name, int[] npc)
    {
        m_quest_name = name;
        m_npc_id = npc;
    }

    void SetQuestData(string name, int[] npc)
    {
        this.m_quest_name = name;
        this.m_npc_id = npc;
    }
}
