public class PlayerData
{
    public Character m_character_type;
    public playerStatus m_player_status;
    public int m_talk_id;
    public int m_talk_action_id;
    public int m_quest_id;
    public int m_quest_action_id;

    public void SetPlayerData()
    {
        m_character_type = new Character();
        m_player_status = new playerStatus();
        
        m_talk_id = 0;
        m_talk_action_id = 0;
        m_quest_id = 1000;
        m_quest_action_id = 0;
    }
}
