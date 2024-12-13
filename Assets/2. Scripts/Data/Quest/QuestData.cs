using UnityEngine;
namespace Taekyung
{

    [System.Serializable]
    public class QuestData
    {
        public string m_quest_id;
        public string m_quest_name;
        public string[] m_quest_data;
    }

    [System.Serializable]
    public class QuestDataWrapper
    {
        public QuestData[] m_quest_datas;
    }
}
