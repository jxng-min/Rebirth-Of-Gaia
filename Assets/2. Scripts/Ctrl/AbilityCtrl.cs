using UnityEngine;
using TMPro;

namespace Jongmin
{
    public class AbilityCtrl : MonoBehaviour
    {
        [Header("SaveManager")]
        [SerializeField]
        private SaveManager m_save_manager;

        [SerializeField]
        private GameObject[] m_portraits;

        [SerializeField]
        private TMP_Text m_strength_state;
        
        [SerializeField]
        private TMP_Text m_intellect_state;

        [SerializeField]
        private TMP_Text m_sociality_state;

        [SerializeField]
        private TMP_Text m_stamina_state;

        [SerializeField]
        private TMP_Text m_defense_state;

        private void Start()
        {
            for(int i = 0; i < m_portraits.Length; i++)
            {
                m_portraits[i].SetActive(false);
            }

            switch(GameManager.Instance.CharacterType)
            {
                case Character.SOCIA:
                    m_portraits[0].SetActive(true);
                    break;

                case Character.GOV:
                    m_portraits[1].SetActive(true);
                    break;

                case Character.ENVA:
                    m_portraits[2].SetActive(true);
                    break;
            }
        }

        private void Update()
        {
            ShowAllAbility();
        }

        private void ShowStrengthAbility()
        {
            m_strength_state.text = m_save_manager.Player.m_player_status.m_strength.ToString("F1");
        }

        private void ShowIntellectAbility()
        {
            m_intellect_state.text = m_save_manager.Player.m_player_status.m_intellect.ToString("F1");
        }

        private void ShowSocialityAbility()
        {
            m_sociality_state.text = m_save_manager.Player.m_player_status.m_sociality.ToString("F1");
        }

        private void ShowStaminaAbility()
        {
            m_stamina_state.text = m_save_manager.Player.m_player_status.m_stamina.ToString("F1");
        }

        private void ShowDefenseAbility()
        {
            m_defense_state.text = m_save_manager.Player.m_player_status.m_defense.ToString("F1");
        }

        // 모든 능력을 출력하는 메소드
        public void ShowAllAbility()
        {
            ShowStrengthAbility();
            ShowIntellectAbility();
            ShowSocialityAbility();
            ShowStaminaAbility();
            ShowDefenseAbility();
        }
    }
}

