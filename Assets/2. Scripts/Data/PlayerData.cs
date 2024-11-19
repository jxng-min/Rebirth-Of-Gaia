using System.Diagnostics;
using UnityEngine;

public class PlayerData
{
    
    public playerStatus m_player_status;

    public int m_talk_idx;
    public int m_talk_id;
    public int m_talk_action_id;
    public int m_quest_id;
    public int m_quest_action_id;

    public void SetPlayerData()
    {
        m_player_status = new playerStatus();
        m_talk_idx = 0;
        m_talk_id = 0;
        m_talk_action_id = 0;
        m_quest_id = 1000;
        m_quest_action_id = 0;
    }

    public void ReceiveTalkId(int id)
    {
        m_talk_id = id;
        UnityEngine.Debug.Log($"받은데이터: {m_talk_id}");
    }
}
