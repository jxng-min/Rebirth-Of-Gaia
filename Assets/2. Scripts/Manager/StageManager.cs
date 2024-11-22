using Jongmin;
using System.Collections.Generic;
using System.IO;
using Taekyung;
using UnityEngine;

namespace Junyoung
{

    public class StageManager : MonoBehaviour
    {
        [SerializeField]
        private CameraMoveCtrl m_camera_move_ctrl;    
        public GameObject m_player;
        public List<StageData> m_stages_data;
        public int m_current_stage_index = 0;

        public PlayerData m_player_data;
        public TalkManager m_talk_manager;

        void Start()
        {
            m_camera_move_ctrl = Camera.main.GetComponent<CameraMoveCtrl>();

            m_player = GameObject.FindGameObjectWithTag("Player");

            LoadStagesData("StageData.json");
            LoadStage(m_current_stage_index);
        }

        private void LoadStagesData(string fileName)
        {
            string file_path = Path.Combine(Application.streamingAssetsPath, fileName);

            if (!File.Exists(file_path))
            {
                Debug.LogError($"JSON 파일이 없음 {file_path}");
                return;
            }

            string jsonData = File.ReadAllText(file_path);
            StageDataWrapper wrapper = JsonUtility.FromJson<StageDataWrapper>(jsonData);
            if (wrapper == null || wrapper.StageData == null)
            {
                Debug.LogError("JSON 파싱 실패, 데이터가 유효하지 않음");
                return;
            }
            m_stages_data = new List<StageData>(wrapper.StageData);


            Debug.Log(jsonData);

            Debug.Log("스테이지 데이터 로드 성공");
        }
        
        public void LoadStage(int stage_index)
        {
            if (stage_index < 0 || stage_index >= m_stages_data.Count)
            {
                Debug.LogError("잘못된 스테이지 인덱스");
                return;
            }

            StageData stageData = m_stages_data[stage_index];

            // 플레이어 위치 설정
            m_player.transform.position = new Vector3(
                stageData.m_player_start_position.x,
                stageData.m_player_start_position.y,
                m_player.transform.position.z
            );

            // 카메라 제한 설정
            m_camera_move_ctrl.m_camera_limit_center = stageData.m_camera_limit_center;
            m_camera_move_ctrl.m_camera_limit_size = stageData.m_camera_limit_size;

            Debug.Log($"스테이지 {stage_index + 1} 로드");
        }

        public void LoadNextStage()
        {
            
            if (m_current_stage_index+ 1 < m_stages_data.Count)
            {

                m_player_data.m_stage_id = m_current_stage_index;
                m_talk_manager.ChangeTalkScene();

                m_current_stage_index++;

                LoadStage(m_current_stage_index);
            }
            else
            {
                Debug.Log("스테이지가 더 존재하지 않음");
            }
        }
    }
}