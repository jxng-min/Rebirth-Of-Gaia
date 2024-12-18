using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

namespace Jongmin
{
    public class ExpCtrl : MonoBehaviour
    {
        private void Update()
        {
            UpdateExp();
        }

        public void UpdateExp()
        {
            int current_lv = SaveManager.Instance.Player.m_player_status.m_current_level;

            if(SaveManager.Instance.Player.m_player_status.m_current_exp >= ExpData.m_exps[current_lv - 1])
            {
                SaveManager.Instance.Player.m_player_status.m_current_level++;
                SaveManager.Instance.Player.m_player_status.m_current_exp -= ExpData.m_exps[current_lv - 1];

                SaveManager.Instance.Player.m_player_status.m_stat_token += 3;

                SaveManager.Instance.Player.m_player_status.m_stamina += SaveManager.Instance.CharacterStatuses[Convert.ToInt32(GameManager.Instance.CharacterType)].GrowthStamina[current_lv - 1];
                SaveManager.Instance.Player.m_player_status.m_defense += SaveManager.Instance.CharacterStatuses[Convert.ToInt32(GameManager.Instance.CharacterType)].GrowthDefense[current_lv - 1];
            }
        }
    }
}