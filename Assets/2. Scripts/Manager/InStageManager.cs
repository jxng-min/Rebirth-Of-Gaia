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

        public int m_total_enemy_num { get; set; }
        public int m_killed_enemy_num { get; set; } = 0;





    }
}


