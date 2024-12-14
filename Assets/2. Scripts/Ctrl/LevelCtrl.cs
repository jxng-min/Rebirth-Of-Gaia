using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Jongmin
{
    public class LevelCtrl : MonoBehaviour
    {
        [Header("SaveManager")]
        [SerializeField]
        private SaveManager m_save_manager;
        
        [Header("LV / EXP UI")]
        [SerializeField]
        private Slider m_exp_bar;
        [SerializeField]
        private TMP_Text m_level_text;
        [SerializeField]
        private TMP_Text m_exp_text;

        private void Update()
        {
            if(m_save_manager)
            {
                UpdateLevel();
            }
        }

        public void UpdateLevel()
        {
            float current_exp = (float)m_save_manager.Player.m_player_status.m_current_exp;
            float max_exp = (float)ExpData.m_exps[m_save_manager.Player.m_player_status.m_current_level - 1];

            m_level_text.text = $"Lv.{m_save_manager.Player.m_player_status.m_current_level.ToString()}";
            m_exp_text.text = $"({m_save_manager.Player.m_player_status.m_current_exp} / {ExpData.m_exps[m_save_manager.Player.m_player_status.m_current_level - 1]})"; 

            m_exp_bar.value =  current_exp / max_exp;
        }
    }
}