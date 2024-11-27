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
        

        private List<StageData> m_stages_data;
        
        [Header("Stage UI")]
        [SerializeField]
        private Button[] m_select_buttons; //인스펙터에서 연결

        [SerializeField]
        private RectTransform[] m_select_buttons_pos_list;

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


        [SerializeField] private int m_Arrival_button_index=0;
        [SerializeField] private int m_now_button_index = 0;
        private List<int> m_path_list = new List<int>();
        private int m_current_path_index = 0;
        private bool m_is_icon_Arrival=false;
        private bool m_is_icon_moving = false;
        private float m_icon_move_speed = 5f;
       
        
       


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
            
        }

        private void FixedUpdate()
        {
            if (!m_is_icon_moving || m_path_list.Count == 0) return;

            int target_index = m_path_list[m_current_path_index]; // 만들어진 경로 리스트를 path_index값을 따라서 하나씩 이동
            Vector2 target_pos = m_select_buttons_pos_list[target_index].anchoredPosition;
            
            target_pos = new Vector2(target_pos.x, target_pos.y+ 70); // 아이콘이 버튼을 가리지 않도록 70만큼 offset
          



            //아이콘이 클릭한 버튼 위치로 이동
            m_player_icon.anchoredPosition = Vector2.MoveTowards(m_player_icon.anchoredPosition, target_pos, m_icon_move_speed);

            //아이콘이 버튼 위치에 도착했는지 체크
            if (m_player_icon.anchoredPosition == target_pos)
            {
                if(m_current_path_index<9) // 스테이지 인덱스의 최대 값은 9
                    m_current_path_index++;

                if (!m_is_icon_Arrival && m_current_path_index >= m_Arrival_button_index) // false일때만 true로 바꾸고 한번 호출하기 때문에 같은 위치에 있더라도 계속 호출하지 않음
                {
                    m_is_icon_Arrival= true;
                    m_is_icon_moving = false;
                    m_stage_select_ckeck_UI.SetActive(true);
                    m_now_button_index = m_Arrival_button_index;
                    Debug.Log($"아이콘이 버튼 {m_now_button_index}에 도달");
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


        public void ClickedButtonPosiotion(int button_index) //클릭된 버튼의 index값을 불러와서 지금 위치와 가려는 위치를 비교하여 정방향/역방향 경로를 리스트에 추가
        {
            m_path_list.Clear();
            m_current_path_index = 0; 

            if (m_now_button_index < button_index) // 지금 위치보다 클릭한 버튼이 뒤면 정방향
            {
                for (int i = m_now_button_index + 1; i <= button_index; i++)
                {
                    m_path_list.Add(i);
                }
            }
            else if (m_now_button_index > button_index) // 역방향
            {
                for (int i = m_now_button_index - 1; i >= button_index; i--)
                {
                    m_path_list.Add(i);
                }
            }
            else
                m_path_list.Add(m_now_button_index); // 같은 버튼을 클릭한 경우 현재 위치 index를 넘겨줘서 그자리에 있도록
            m_Arrival_button_index = button_index;
            m_is_icon_moving = true;
            Debug.Log($"아이콘이 버튼 {m_now_button_index}에서 버튼 {m_Arrival_button_index}까지 이동 시작");
        }
    }
}