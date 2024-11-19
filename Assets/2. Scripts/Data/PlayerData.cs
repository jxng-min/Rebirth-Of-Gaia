using System.Diagnostics;
using UnityEngine;

public class PlayerData
{
    public Character m_character_type;
    public playerStatus m_player_status;

    public int m_talk_idx; // 현재 대사의 몇번째 줄인지
    public int m_talk_id; // 어떤 대사를 말하는지
    public int m_talk_action_id; // 대사를 말하고 있는지
    public int m_quest_id; // 몇번째 퀘스트를 하고 있는지
    public int m_quest_action_id; // 퀘스트를 진행하고 있는지

    public void SetPlayerData()
    {
        m_character_type = new Character();
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
        UnityEngine.Debug.Log($"현재 대사 번호: {m_talk_id}");
    }
}
