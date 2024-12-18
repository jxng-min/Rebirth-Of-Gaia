using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Jongmin
{
    public class LevelCtrl : MonoBehaviour
    {
        [Header("LV / EXP UI")]
        [SerializeField]
        private Slider m_exp_bar;
        [SerializeField]
        private TMP_Text m_level_text;
        [SerializeField]
        private TMP_Text m_exp_text;

        private void Update()
        {
            UpdateLevel();
        }

        public void UpdateLevel()
        {
            float current_exp = (float)SaveManager.Instance.Player.m_player_status.m_current_exp;
            float max_exp = (float)ExpData.m_exps[SaveManager.Instance.Player.m_player_status.m_current_level - 1];

            m_level_text.text = $"Lv.{SaveManager.Instance.Player.m_player_status.m_current_level.ToString()}";
            m_exp_text.text = $"({SaveManager.Instance.Player.m_player_status.m_current_exp} / {ExpData.m_exps[SaveManager.Instance.Player.m_player_status.m_current_level - 1]})"; 

            m_exp_bar.value =  current_exp / max_exp;
        }
    }
}