using UnityEngine;

namespace Jongmin
{
    [System.Serializable]
    public class TalkData
    {
        public ObjectData m_object_data;
        public string[] m_talk_data;
    }

    [System.Serializable]
    public class TalkDataWrapper
    {
        public TalkData[] m_talk_datas;
    }

    [System.Serializable]
    public class QuestData
    {
        public ObjectData m_object_data;
        public string[] m_talk_data;
    }

    [System.Serializable]
    public class QuestDataWrapper
    {
        public QuestData[] m_quest_datas;
    }

    [System.Serializable]
    public class QuestList
    {
        public int m_quest_id;
        public Taekyung.QuestData m_quest_list;
    }

    [System.Serializable]
    public class QuestListWrapper
    {
        public QuestList[] m_quest_lists;
    }
}
