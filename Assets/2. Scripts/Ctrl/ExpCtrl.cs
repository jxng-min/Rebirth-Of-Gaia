using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Jongmin
{
    public class ExpCtrl : MonoBehaviour
    {
        [Header("SaveManager")]
        [SerializeField]
        private SaveManager m_save_manager;
        
        private Slider m_exp_bar;
        private TMP_Text m_level_text;

        private void Start()
        {
            m_exp_bar = GetComponentInChildren<Slider>();
            m_level_text = GetComponentInChildren<TMP_Text>();
        
            m_level_text.text = m_save_manager.Player.m_player_status.m_current_level.ToString();
        }

        public void FixedUpdate()
        {
            //UpdateExp();
            //LevelUp();
        }

        // EXP 바를 주기적으로 갱신하는 메소드
        private void UpdateExp()
        {
            float current_exp = (float)m_save_manager.Player.m_player_status.m_current_exp;
            float max_exp = (float)ExpData.m_exps[m_save_manager.Player.m_player_status.m_current_level - 1];

            // Mathf.Lerp를 이용한 수정이 좀 필요할 수도 있음
            m_exp_bar.value =  current_exp / max_exp;
        }

        // 레벨 업을 위한 메소드
        private void LevelUp()
        {
            int current_lv = m_save_manager.Player.m_player_status.m_current_level;
            int current_exp = m_save_manager.Player.m_player_status.m_current_exp;

            if(current_exp >= ExpData.m_exps[current_lv - 1])
            {
                m_save_manager.Player.m_player_status.m_current_level++;
                m_level_text.text = (current_lv + 1).ToString();

                m_save_manager.Player.m_player_status.m_current_exp -= ExpData.m_exps[current_lv - 1];

                m_save_manager.Player.m_player_status.m_stat_token += 3;
            }
        }

        // 테스트를 위한 임시 테스트 메소드
        public void TestButton()
        {
            m_save_manager.Player.m_player_status.m_current_exp += 1;
        }
    }
}