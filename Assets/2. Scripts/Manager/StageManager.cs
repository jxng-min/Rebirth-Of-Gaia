using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Junyoung
{

    public class StageManager : MonoBehaviour
    {
        public CameraMoveCtrl m_camera_move_ctrl;
        public GameObject m_player;
        private List<StageData> m_stages_data; // 스테이지 데이터 리스트
        public int m_current_stage_index { get; private set; } = 0;


        void Start()
        {
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
            m_stages_data = new List<StageData>(wrapper.stages); // 리스트로 변환

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
        void Update()
        {

        }
    }
}


