using Jongmin;
using System.Collections.Generic;
using System.IO;
using Taekyung;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Junyoung
{

    public class StageManager : MonoBehaviour
    {
        //private int m_current_stage_index = 0;

        private List<StageData> m_stages_data;
        
        [Header("Stage UI")]
        [SerializeField]
        private Button[] m_select_buttons; //인스펙터에서 연결

        [SerializeField]
        private GameObject m_stage_select_UI;//인스펙터에서 연결

        [SerializeField]
        private GameObject m_stage_select_ckeck_UI;//인스펙터에서 연결

        [SerializeField]
        private TMP_Text m_ui_text;//인스펙터에서 연결

        [SerializeField]
        private RectTransform m_player_icon;//인스펙터에서 연결

        private int m_stage_index;

        private GameObject m_player;

        private bool m_is_icon_Arrival=false;
        private float m_icon_move_speed = 5f;
        private Vector2 m_button_position;
        


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

            m_button_position = m_player_icon.anchoredPosition;

            

            LoadStagesData("StageData.json");
            //LoadStage(m_current_stage_index);
        }

        private void FixedUpdate()
        {
            //아이콘이 클릭한 버튼 위치로 이동
            m_player_icon.anchoredPosition = Vector2.MoveTowards(m_player_icon.anchoredPosition, m_button_position, m_icon_move_speed);

            //아이콘이 버튼 위치에 도착했는지 체크
            if (m_player_icon.anchoredPosition == m_button_position)
            {
                if(!m_is_icon_Arrival) // false일때만 true로 바꾸고 한번 호출하기 때문에 같은 위치에 있더라도 계속 호출하지 않음
                {
                    m_is_icon_Arrival= true;
                    m_stage_select_ckeck_UI.SetActive(true);
                }
            }
                
            else
                m_is_icon_Arrival = false;


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
        
        public void LoadStage(int stage_index) //버튼에서 로드할 경우 인덱스는 인스펙터에서 버튼마다 직접 할당
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

            if (m_stage_select_ckeck_UI.activeSelf)
                m_stage_select_ckeck_UI.SetActive(false);


        }

        public void StageSelect(int stage_index)
        {           
            m_stage_index = stage_index;
            m_is_icon_Arrival = false; // 버튼을 두번 째 누를경우 false로 바꿔야 UI를 활성화 가능
            m_ui_text.text = $"Do you want to go to Stage {m_stage_index+1} ?";

        }

        public void StageSelectYes() 
        {
            Debug.Log($"스테이지 선택 예 클릭");
            LoadStage(m_stage_index);
            m_stage_select_ckeck_UI.SetActive(false);
            StageSelectPanelOnoff();
        }

        public void StageSelectNo()
        {
            Debug.Log($"스테이지 선택 아니오 클릭");
            m_stage_select_ckeck_UI.SetActive(false);
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


        public void ClickedButtonPosiotion(Button clickedButton) // 메서드가 호출되면 매개변수에 들어온 Button의 RectTransform값을 전달(매개변수는 인스펙터 창에서 버튼 자신을 넣어줌)
        {
            RectTransform clicked_position = clickedButton.GetComponent<RectTransform>();
            if(clicked_position != null)
            {
                Vector2 offset_position = clicked_position.anchoredPosition;
                offset_position.y += 70f;
                m_button_position = offset_position;
                Debug.Log($"버튼이 클릭됨 아이콘이 버튼 위치 y값 + 70 위치로 이동");
            }


        }


    }
}