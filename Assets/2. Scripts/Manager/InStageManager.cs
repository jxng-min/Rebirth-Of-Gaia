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

        [Header("SpawnEnemy")]
        [SerializeField]
        private List<EnemyStatus> m_enemy_status_list;
        [SerializeField]
        private List<GameObject> m_enemy_prefab;
        public Vector3[] m_enemy_spawn_pos;



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
                
                m_save_manager.Player.m_stage_state = 1;
                m_talk_manager.ChangeTalkScene();
            }                
            else
            {
                Debug.Log($"최대 스테이지({m_stage_manager.m_max_stage})까지 클리어");
                return;
            }
            m_save_manager.SaveData();
            Debug.Log($"스테이지 클리어. m_max_clear_stage를 {m_save_manager.Player.m_max_clear_stage}로 변경");
        }

        public void SpawnEnemy(EnemyType type, int transform_index)
        {
            var newEnemy = Instantiate(m_enemy_prefab[(int)type]).GetComponent<EnemyCtrl>(); // 생성된 객체의 스크립트를 가져옴
            newEnemy.m_enemy_status = m_enemy_status_list[m_save_manager.Player.m_stage_id];
            newEnemy.name = newEnemy.m_enemy_status.EnemyName;
            Debug.Log($"{type}타입의 적 생성위치{transform_index}에서 생성");
            newEnemy.transform.position = m_enemy_spawn_pos[transform_index];
            
        }

    }
}


