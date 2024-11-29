using UnityEngine;
using TMPro;

namespace Jongmin
{
    public class AbilityCtrl : MonoBehaviour
    {
        [Header("SaveManager")]
        [SerializeField]
        private SaveManager m_save_manager;

        [Header("Ability UI")]
        [SerializeField]
        private GameObject m_ability_panel;

        [SerializeField]
        private TMP_Text m_character_name;

        [SerializeField]
        private TMP_Text m_strength_state;
        
        [SerializeField]
        private TMP_Text m_intellect_state;

        [SerializeField]
        private TMP_Text m_sociality_state;

        private void OnEnable()
        {
            ShowAllAbility();
        }

        private void SetPlayerName()
        {
            m_character_name.text = m_save_manager.Player.m_character_type.ToString();
        }

        // 공격력을 출력하는 메소드
        private void ShowStrengthAbility()
        {
            m_strength_state.text = m_save_manager.Player.m_player_status.m_strength.ToString();
        }

        // 마력을 출력하는 메소드
        private void ShowIntellectAbility()
        {
            m_intellect_state.text = m_save_manager.Player.m_player_status.m_intellect.ToString();
        }

        // 사회력을 출력하는 메소드
        private void ShowSocialityAbility()
        {
            m_sociality_state.text = m_save_manager.Player.m_player_status.m_sociality.ToString();
        }

        // 모든 능력을 출력하는 메소드
        public void ShowAllAbility()
        {
            ShowStrengthAbility();
            ShowIntellectAbility();
            ShowSocialityAbility();
        }

        public void DeactivateUI()
        {
            GameEventBus.Publish(GameEventType.PLAYING);
            m_ability_panel.SetActive(false);
        }

        public void ActivateUI()
        {
            GameEventBus.Publish(GameEventType.SETTING);
            m_ability_panel.SetActive(true);
        }
    }
}

