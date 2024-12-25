using Jongmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkUIManager : MonoBehaviour
{
    [Header("Talk UI")]
    [SerializeField]
    private GameObject m_talk_ui;
    
    [SerializeField]
    private Image m_npc_img;
    
    [SerializeField]
    private Image m_player_img;
    
    [SerializeField]
    private GameObject m_end_cursor;

    public void UpdateTalkUI(Sprite portrait, bool is_player)
    {
        
        UpdatePlayerPortrait(is_player, portrait);
        UpdateNpcPortrait(portrait, is_player);
    }

    // 대화 UI에서 플레이어 초상화의 투명도를 조절하는 메소드
    private void UpdatePlayerPortrait(bool is_player, Sprite portrait)
    {
        if(is_player)
        {
            m_player_img.sprite = portrait;
            m_player_img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            m_player_img.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        }
    }

    // 대화 UI에서 NPC 초상화의 투명도를 조절하는 메소드
    private void UpdateNpcPortrait(Sprite portrait, bool is_player)
    {
        if (portrait != null)
        {
            if (is_player)
            {
                m_npc_img.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            }
            else
            {
                m_npc_img.sprite = portrait;
                m_npc_img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            m_npc_img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    // 대화 UI의 상태를 활성화하는 메소드
    public void SetTalkUIActive(bool is_active)
    {
        m_talk_ui.SetActive(is_active);
    }

    private void SetEndCursor(bool state)
    {
        m_end_cursor.SetActive(state);
    }
}
