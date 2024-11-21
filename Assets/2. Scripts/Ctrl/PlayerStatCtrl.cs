using Jongmin;
using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class PlayerStatCtrl : MonoBehaviour
{
    private SaveManager m_save_manager;
    public TMP_Text m_character_name;
    public TMP_Text m_player_stat;

    private void Start()
    {
        m_save_manager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        
        PlayerStatus player_status = m_save_manager.m_now_player.m_player_status;
        SetPlayerStatUI(player_status.m_attack_power, player_status.m_magic_power, player_status.m_social_influence);
    }

    // 플레이어 스탯이 변경되는 경우에 UI를 업데하는 메소드
    public void SetPlayerStatUI(float ap, float mp, float si)
    {
        m_character_name.text = m_save_manager.m_now_player.m_character_type.ToString();
        m_player_stat.text = $"AP: {ap}\nMP: {mp}\nSI: {si}";
    }
}
