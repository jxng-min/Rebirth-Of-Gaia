using UnityEngine;

namespace Jongmin
{
    [System.Serializable]
    public class TalkData
    {
        public string m_stage_data;
        public string[] m_talk_data;
    }

    [System.Serializable]
    public class TalkDataWrapper
    {
        public TalkData[] m_talk_datas;
    }
}
