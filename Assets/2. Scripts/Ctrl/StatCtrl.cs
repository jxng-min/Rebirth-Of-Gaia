using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jongmin
{
    public class StatCtrl : MonoBehaviour
    {
        public GameObject m_stat_panel;
        public Button[] m_upgrade_buttons = new Button[3];
        public TMP_Text m_token_count_text;

        private SaveManager m_save_manager;

        private void OnEnable()
        {
            if(m_save_manager == null)
            {
                m_save_manager = FindAnyObjectByType<SaveManager>();
            }
        }

        private void Update()
        {
            SetTokenText();
            SetButtonState();
        }

        private void SetTokenText()
        {
            m_token_count_text.text = m_save_manager.m_now_player.m_player_status.m_stat_token.ToString();
        }

        // 토큰 개수에 따라 누를 수 있는 버튼의 상태를 업데이트하는 메소드
        private void SetButtonState()
        {
            int stat_token = m_save_manager.m_now_player.m_player_status.m_stat_token;
            
            if(stat_token <= 0)
            {
                for(int i = 0; i < m_upgrade_buttons.Length; i++)
                {
                    m_upgrade_buttons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
                    m_upgrade_buttons[i].interactable = false;
                }
            }
            else
            {
                for(int i = 0; i < m_upgrade_buttons.Length; i++)
                {
                    if(!m_upgrade_buttons[i].IsInteractable())
                    {
                        m_upgrade_buttons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                        m_upgrade_buttons[i].interactable = true;
                    }
                }
            }
        }

        // 공격력을 업그레이드하는 버튼에 적용되는 메소드
        public void StrengthUpgradeButton()
        {
            int stat_token = m_save_manager.m_now_player.m_player_status.m_stat_token;
            
            if(stat_token <= 0)
            {
                Debug.Log("토큰이 없어서 공격력을 업그레이드할 수 없습니다.");
                return;
            }

            m_save_manager.m_now_player.m_player_status.m_stat_token--;
            m_save_manager.m_now_player.m_player_status.m_ap_enforce++;

            m_save_manager.m_now_player.m_player_status.m_strength += 2;
        }

        // 마력을 업그레이드하는 버튼에 적용되는 메소드
        public void IntellectUpgradeButton()
        {
            int stat_token = m_save_manager.m_now_player.m_player_status.m_stat_token;
            
            if(stat_token <= 0)
            {
                Debug.Log("토큰이 없어서 지력을 업그레이드할 수 없습니다.");
                return;
            }

            m_save_manager.m_now_player.m_player_status.m_stat_token--;
            m_save_manager.m_now_player.m_player_status.m_mp_enforce++;
            m_save_manager.m_now_player.m_player_status.m_intellect += 2;
        }

        // 사회력을 업그레이드하는 버튼에 적용되는 메소드
        public void SocialityUpgradeButton()
        {
            int stat_token = m_save_manager.m_now_player.m_player_status.m_stat_token;
            
            if(stat_token <= 0)
            {
                Debug.Log("토큰이 없어서 사회성을 업그레이드할 수 없습니다.");
                return;
            }

            m_save_manager.m_now_player.m_player_status.m_stat_token--;
            m_save_manager.m_now_player.m_player_status.m_si_enforce++;
        }

        public void DeactivateUI()
        {
            m_stat_panel.SetActive(false);
        }

        public void ActivateUI()
        {
            m_stat_panel.SetActive(true);
        }
    }
}