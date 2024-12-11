using Jongmin;
using Taekyung;
using UnityEngine;
using System.Collections.Generic;

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
        [SerializeField]
        private TalkManager m_talk_manager;

        public void ResetClearStage()
        {            
            m_save_manager.Player.m_max_clear_stage=-1;
            m_stage_manager.SelectButtonReset();
            Debug.Log($"클리어 스테이지 초기화. m_max_clear_stage를 {m_save_manager.Player.m_max_clear_stage}로 변경");
        }

        public void Clear()
        {
            if(m_save_manager.Player.m_max_clear_stage< m_stage_manager.m_max_stage)
            {
                m_save_manager.Player.m_max_clear_stage++;
                
            }                
            else
            {
                Debug.Log($"최대 스테이지({m_stage_manager.m_max_stage})까지 클리어");
                return;
            }
            m_save_manager.SaveData();
            GameEventBus.Publish(GameEventType.CLEAR);//게임 이벤트버스로 CLEAR이벤트 호출
            Debug.Log($"스테이지 클리어. m_max_clear_stage를 {m_save_manager.Player.m_max_clear_stage}로 변경");
        }
    }
}


