using Jongmin;
using System.Collections.Generic;
using System.IO;
using Taekyung;
using UnityEngine;
using UnityEngine.UI;

namespace Junyoung
{

    public class StageManager : MonoBehaviour
    {
        private int m_current_stage_index = 0;

        private List<StageData> m_stages_data;
        
        [Header("Stage UI")]
        [SerializeField]
        private Button[] m_select_buttons;

        [SerializeField]
        private GameObject m_stage_select_UI;
        
        private GameObject m_player;

        [Header("Managers")]
        [SerializeField]
        private TalkManager m_talk_manager;

        [SerializeField]
        private SaveManager m_save_manager;

        private CameraMoveCtrl m_camera_move_ctrl;

        void Start()
        {
            m_camera_move_ctrl = Camera.main.GetComponent<CameraMoveCtrl>();

            m_player = GameObject.FindGameObjectWithTag("Player");           
            m_save_manager = GameObject.FindAnyObjectByType<SaveManager>();
            
            LoadStagesData("StageData.json");
            LoadStage(m_current_stage_index);
        }

        private void LoadStagesData(string file_name)
        {
            string file_path = Path.Combine(Application.streamingAssetsPath, file_name);

            if (!File.Exists(file_path))
            {
                Debug.LogError($"JSON 파일이 없음 {file_path}");
                return;
            }

            string json_data = File.ReadAllText(file_path);

            StageDataWrapper wrapper = JsonUtility.FromJson<StageDataWrapper>(json_data);

            if (wrapper == null || wrapper.StageData == null)
            {
                Debug.LogError("JSON 파싱 실패, 데이터가 유효하지 않음");
                return;
            }
            m_stages_data = new List<StageData>(wrapper.StageData);


            Debug.Log(json_data);

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
            m_camera_move_ctrl.CameraLimitCenter = stageData.m_camera_limit_center;
            m_camera_move_ctrl.CameraLimitSize = stageData.m_camera_limit_size;

            Debug.Log($"스테이지 {stage_index} 로드");


            m_save_manager.Player.m_stage_id = stage_index;


            Debug.Log($"m_stage_id : {m_save_manager.Player.m_stage_id}");


        }
        
        public void StageSelectPanelOnoff()
        {
            bool isActive = m_stage_select_UI.activeSelf;
            if ( !isActive )
                Debug.Log($"스테이지 선택창 활성화");
            else
                Debug.Log($"스테이지 선택창 비활성화");
            m_stage_select_UI.SetActive( !isActive );
            
        }

        public void SelectButtonInteract() //스테이지 선택 버튼을 최대 클리어 스테이지 +1 만큼 활성화 
        {
            m_select_buttons[m_save_manager.Player.m_max_clear_stage].interactable = true;
            Debug.Log($"스테이지 {m_save_manager.Player.m_max_clear_stage} 버튼 활성화");
        }

        public void SelectButtonReset() //스테이지 선택 버튼을 최대 클리어 스테이지 +1 만큼 활성화 
        {
            for(int i = 1; i <= 9; i++)
            {
                m_select_buttons[i].interactable = false;
            }
            
            Debug.Log($"스테이지 선택 버튼 비활성화");
        }
    }
}