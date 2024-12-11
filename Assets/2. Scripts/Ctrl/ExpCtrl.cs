using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

namespace Jongmin
{
    public class ExpCtrl : MonoBehaviour
    {
        [SerializeField]
        private SaveManager m_save_manager;

        private void Update()
        {
            UpdateExp();
        }

        public void UpdateExp()
        {
            int current_lv = m_save_manager.Player.m_player_status.m_current_level;

            if(m_save_manager.Player.m_player_status.m_current_exp >= ExpData.m_exps[current_lv - 1])
            {
                m_save_manager.Player.m_player_status.m_current_level++;
                m_save_manager.Player.m_player_status.m_current_exp -= ExpData.m_exps[current_lv - 1];

                m_save_manager.Player.m_player_status.m_stat_token += 3;

                m_save_manager.Player.m_player_status.m_stamina += m_save_manager.CharacterStatuses[Convert.ToInt32(GameManager.Instance.CharacterType)].GrowthStamina[current_lv - 1];
                m_save_manager.Player.m_player_status.m_defense += m_save_manager.CharacterStatuses[Convert.ToInt32(GameManager.Instance.CharacterType)].GrowthDefense[current_lv - 1];
            }
        }

        public void TestButton()
        {
            m_save_manager.Player.m_player_status.m_current_exp += 30;
        }
    }
}