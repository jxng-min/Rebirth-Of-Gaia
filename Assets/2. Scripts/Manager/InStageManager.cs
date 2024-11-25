using Jongmin;
using UnityEngine;

namespace Junyoung
{
    public class InStageManager : MonoBehaviour
    {
        private StageManager m_stage_manager;
        private SaveManager m_save_manager;

        void Start()
        {
            m_save_manager = GameObject.FindAnyObjectByType<SaveManager>();
            m_stage_manager = GameObject.FindAnyObjectByType<StageManager>(); 
        }

        public void ResetClearStage()
        {
            m_save_manager.m_now_player.m_max_clear_stage=0;
            m_stage_manager.SelectButtonReset();
            Debug.Log($"클리어 스테이지 초기화. m_max_clear_stage를 {m_save_manager.m_now_player.m_max_clear_stage}로 변경");
        }

        public void Clear()//스테이지가 클리어되면 최대 스테이지 값을 변경
        {
            m_save_manager.m_now_player.m_max_clear_stage++;
            m_save_manager.SaveData();
            m_stage_manager.SelectButtonInteract();
            Debug.Log($"스테이지 클리어. m_max_clear_stage를 {m_save_manager.m_now_player.m_max_clear_stage}로 변경");
        }

    }
}


