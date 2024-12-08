using UnityEngine;
namespace Taekyung
{

    [System.Serializable]
    public class QuestData
    {
        public string m_quest_name;
        public int m_npc_id;
        public string[] m_quest_data;
    }

    [System.Serializable]
    public class QuestDataWrapper
    {
        public QuestData[] m_quest_datas;
    }
}
