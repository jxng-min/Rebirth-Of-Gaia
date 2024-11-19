namespace Taekyung
{
    public class QuestData
    {
        public string m_quest_name;
        public int m_npc_id;

        QuestData(string name, int npc)
        {
            m_quest_name = name;
            m_npc_id = npc;
        }

        void SetQuestData(string name, int npc)
        {
            this.m_quest_name = name;
            this.m_npc_id = npc;
        }
    }
}