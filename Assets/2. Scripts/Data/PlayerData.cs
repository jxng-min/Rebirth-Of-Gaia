
namespace Jongmin
{
    [System.Serializable]
    public class PlayerData
    {
        public Character m_character_type;
        public PlayerStatus m_player_status;
        public int m_talk_id;
        public int m_talk_action_id;
        public int m_quest_id;
        public int m_quest_action_id;

        public PlayerData(Character char_type, PlayerStatus player_status)
        {
            m_character_type = char_type;
            m_player_status = player_status;
            
            m_talk_id = 0;
            m_talk_action_id = 0;
            m_quest_id = 0;
            m_quest_action_id = 0;
        }
    }
}