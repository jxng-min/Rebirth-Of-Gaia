using Jongmin;
using UnityEngine;

namespace Junyoung
{
    public class InStageManager : MonoBehaviour
    {
        [Header("StageManager")]
        [SerializeField]
        private StageManager m_stage_manager;

        [Header("SaveManager")]
        [SerializeField]
        private SaveManager m_save_manager;

        public void ResetClearStage()
        {
            m_save_manager.m_now_player.m_max_clear_stage=0;
            m_stage_manager.SelectButtonReset();
            Debug.Log($"클리어 스테이지 초기화. m_max_clear_stage를 {m_save_manager.m_now_player.m_max_clear_stage}로 변경");
        }

        public void Clear()
        {
            m_save_manager.m_now_player.m_max_clear_stage++;
            m_save_manager.SaveData();
            m_stage_manager.SelectButtonInteract();
            Debug.Log($"스테이지 클리어. m_max_clear_stage를 {m_save_manager.m_now_player.m_max_clear_stage}로 변경");
        }

    }
}


