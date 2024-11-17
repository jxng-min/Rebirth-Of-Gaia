using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkUiManager : MonoBehaviour
{
    // 데이터 관리
    public GameObject m_talk_ui;
    public Image m_npc_img;
    public Image m_player_img;
    public Text m_text;

    // 초상화 껏다 키기, text 초기화
    public void UpdateTalkUi(string text, Sprite portrait, bool is_npc, bool is_player)
    {
        m_text.text = text;
        
        UpdatePlayerPortrait(is_player, portrait);
        UpdateNpcPortrait(is_npc, portrait, is_player);
    }

    // 플레이어 초상화 최신화
    private void UpdatePlayerPortrait(bool is_player, Sprite portrait)
    {
        if(is_player)
        {
            m_player_img.sprite = portrait;
            m_player_img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            m_player_img.color = new Color(1, 1, 1, 0.4f);
        }
    }

    // npc 초상화 최신화
    private void UpdateNpcPortrait(bool is_npc, Sprite portrait, bool is_player)
    {
        if (is_npc && portrait != null)
        {
            m_npc_img.sprite = portrait;
            m_npc_img.color = new Color(1, 1, 1, 1);
            if (is_player)
            {
                m_npc_img.color = new Color(1, 1, 1, 0.4f);
            }
        }
        else
        {
            m_npc_img.color = new Color(1, 1, 1, 0);
        }
    }

    // UI
    public void SetTalkUiActive(bool is_active)
    {
        m_talk_ui.SetActive(is_active);
    }
}
