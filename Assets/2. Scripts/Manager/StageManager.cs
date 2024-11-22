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
        private List<StageData> m_stages_data; // 스테이지 데이터 리스트\
        public int m_current_stage_index = 0;

        public PlayerData m_player_data;
        public TalkManager m_talk_manager;

        void Start()
        {
            m_camera_move_ctrl = Camera.main.GetComponent<CameraMoveCtrl>();
            m_player = GameObject.Find("Player");
            LoadStagesData("StageData.json");
            LoadStage(m_current_stage_index);
        }

        private void LoadStagesData(string fileName)//JSON파일에 저장된 스테이지의 값을 m_stages_data 리스트에 저장함
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (!File.Exists(filePath))
            {
                Debug.LogError($"JSON 파일이 없음 {filePath}");
                return;
            }

            string jsonData = File.ReadAllText(filePath); // 파일 내용 읽기
            StageDataWrapper wrapper = JsonUtility.FromJson<StageDataWrapper>(jsonData); // JSON 파싱
            if (wrapper == null || wrapper.StageData == null) // 이 경우 wrapper.stages가 null이 됨
            {
                Debug.LogError("JSON 파싱 실패, 데이터가 유효하지 않음");
                return;
            }
            m_stages_data = new List<StageData>(wrapper.StageData); // 리스트로 변환


            Debug.Log(jsonData);  // JSON 문자열을 출력하여 값이 제대로 들어갔는지 확인

            Debug.Log("스테이지 데이터 로드 성공");
        }
        public void LoadStage(int stageIndex) //스테이지 데이터 리스트에서 값을 불러옴
        {
            if (stageIndex < 0 || stageIndex >= m_stages_data.Count)
            {
                Debug.LogError("잘못된 스테이지 인덱스");
                return;
            }

            StageData stageData = m_stages_data[stageIndex];

            // 플레이어 위치 설정
            m_player.transform.position = new Vector3(
                stageData.m_player_start_position.x,
                stageData.m_player_start_position.y,
                m_player.transform.position.z
            );

            // 카메라 제한 설정
            m_camera_move_ctrl.m_camera_limit_center = stageData.m_camera_limit_center;
            m_camera_move_ctrl.m_camera_limit_size = stageData.m_camera_limit_size;

            Debug.Log($"스테이지 {stageIndex + 1} 로드");
        }

        public void LoadNextStage()
        {
            
            if (m_current_stage_index+ 1 < m_stages_data.Count)
            {
                m_current_stage_index++;

                m_player_data.m_stage_id = m_current_stage_index;
                m_talk_manager.ChangeTalkScene();

                LoadStage(m_current_stage_index);
            }
            else
            {
                Debug.Log("스테이지가 더 존재하지 않음");
            }
        }
        void Update()
        {
            
        }
    }
}


