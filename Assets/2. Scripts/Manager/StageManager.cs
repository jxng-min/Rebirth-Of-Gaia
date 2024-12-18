using Jongmin;
using System.Collections.Generic;
using System.IO;
using Taekyung;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Junyoung
{
    public class StageManager : MonoBehaviour
    {
        private List<StageData> m_stages_data;
        
        [Header("Stage UI")]
        [SerializeField]
        private RectTransform m_player_icon;
        [SerializeField]
        private Button[] m_select_buttons;
        [SerializeField]
        private GameObject m_stage_select_check_UI;
        [SerializeField]
        private Sprite[] m_stage_status_images;

        [Header("About Index")]
        private int m_current_index;
        public int m_max_stage { get; private set; }


        [SerializeField] 
        private int m_now_button_index = 0;
        private List<int> m_path_list = new List<int>();
        private int m_current_path_index = 0;
        private bool m_is_icon_moving = false;
        private float m_icon_move_speed = 250f;
        
        [Header("Managers")]
        [SerializeField]
        private TalkManager m_talk_manager;
        
        [Header("About InGame")]
        [SerializeField]
        private EnemyFactory m_enemy_factory;
        private GameObject m_player;
        private CameraMoveCtrl m_camera_move_ctrl;
        public int m_total_enemy_num { get; set; }
        public int m_killed_enemy_num { get; set; } = 0;

        private void Start()
        {
            m_max_stage = m_select_buttons.Length;

            m_player = GameObject.FindGameObjectWithTag("Player");
            m_camera_move_ctrl = Camera.main.GetComponent<CameraMoveCtrl>();

            LoadStagesData("StageData.json");
        }

        private void Update()
        {
            for(int i = 0; i < m_select_buttons.Length; i++)
            {
                if(i < SaveManager.Instance.Player.m_max_clear_stage)
                {
                    m_select_buttons[i].interactable = true;
                    m_select_buttons[i].GetComponent<Image>().sprite = m_stage_status_images[2];
                }
                else if(i == SaveManager.Instance.Player.m_max_clear_stage)
                {
                    m_select_buttons[i].interactable = true;
                    m_select_buttons[i].GetComponent<Image>().sprite = m_stage_status_images[1];
                }
                else
                {
                    m_select_buttons[i].interactable = false;
                    m_select_buttons[i].GetComponent<Image>().sprite = m_stage_status_images[0];
                }
            }
        }

        private IEnumerator MoveIconCorutine()
        {
            while (m_is_icon_moving && m_path_list.Count > 0)
            {
                if (m_current_path_index < 0 || m_current_path_index >= m_path_list.Count)
                {
                    Debug.LogError($"m_current_path_index 범위 초과: {m_current_path_index}, m_path_list.Count={m_path_list.Count}");
                    m_is_icon_moving = false;
                    yield break;
                }

                int target_index = m_path_list[m_current_path_index]; // 만들어진 경로 리스트를 path_index값을 따라서 하나씩 이동

                if (target_index < 0 || target_index >= m_select_buttons.Length)
                {
                    Debug.LogError($"target_index 범위 초과: {target_index}, m_select_buttons_pos_list.Length={m_select_buttons.Length}");
                    m_is_icon_moving = false;
                    yield break;
                }
                Debug.Log($"MoveIconCorutine 정상 실행");

                Vector2 target_pos = m_select_buttons[target_index].GetComponent<RectTransform>().anchoredPosition;

                target_pos += new Vector2(0, 70); // 아이콘이 버튼을 가리지 않도록 70만큼 offset


                //아이콘이 클릭한 버튼 위치로 이동 
                // 부동 소수점 오류 때문에 ==로 단순 비교는 오류가 발생 할 수 있음
                while (Vector2.Distance(m_player_icon.anchoredPosition, target_pos) > 0.1f)
                {
                    m_player_icon.anchoredPosition = Vector2.MoveTowards(m_player_icon.anchoredPosition, target_pos, m_icon_move_speed * Time.deltaTime);
                    yield return null; //다음 프레임까지 대기, 프레임 단위로 부드럽게 이동시키기 위해 사용
                }
                
                if (m_current_path_index < m_path_list.Count - 1)
                {
                    m_current_path_index++;
                }
                else
                {
                    m_is_icon_moving = false;
                    m_stage_select_check_UI.SetActive(true);

                    Debug.Log($"아이콘이 버튼 {m_now_button_index}에 도달");
                }
            }
        }

        private void LoadStagesData(string file_name)
        {
            string file_path = Path.Combine(Application.streamingAssetsPath, file_name);

            if (!File.Exists(file_path))
            {
                return;
            }

            string json_data = File.ReadAllText(file_path);

            StageDataWrapper wrapper = JsonUtility.FromJson<StageDataWrapper>(json_data);

            if (wrapper == null || wrapper.StageData == null)
            {
                return;
            }

            m_stages_data = new List<StageData>(wrapper.StageData);
        }
        
        public void LoadStage(int stage_index)
        {
            if (stage_index < 0 || stage_index >= m_stages_data.Count)
            {
                Debug.LogError("잘못된 스테이지 인덱스");
                return;
            }

            FindAnyObjectByType<PersadeCtrl>().UpdatePersade(stage_index);

            StageData stage_data = m_stages_data[stage_index];

            m_player.transform.position = new Vector3(
                                                        stage_data.m_player_start_position.x,
                                                        stage_data.m_player_start_position.y,
                                                        m_player.transform.position.z
                                                     );

            m_camera_move_ctrl.CameraLimitCenter = stage_data.m_camera_limit_center;
            m_camera_move_ctrl.CameraLimitSize = stage_data.m_camera_limit_size;

            m_enemy_factory.m_enemy_spawn_pos[0] = stage_data.m_enemy_spawn_pos1;
            m_enemy_factory.m_enemy_spawn_pos[1] = stage_data.m_enemy_spawn_pos2;
            m_enemy_factory.m_enemy_spawn_pos[2] = stage_data.m_enemy_spawn_pos3;
            m_enemy_factory.m_enemy_spawn_pos[3] = stage_data.m_enemy_spawn_pos4;

            
            m_total_enemy_num = stage_data.m_enemy_spawn_num;

            m_talk_manager.ChangeTalkScene();

            GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);

            SpawnStageEnemy(m_total_enemy_num); 
        }

        private void SpawnStageEnemy(int enemy_count)
        {
            for (int i = 0; i < enemy_count; i++)
            {
                m_enemy_factory.SpawnEnemy(EnemyType.The_Exhausted_Worker, i % 4);
            }
        }

        private void StageSelect(int stage_index)
        {           
            m_current_index = stage_index;            
            m_stage_select_check_UI.GetComponentInChildren<TMP_Text>().text = $"Do you want to go to Stage {m_current_index + 1}?";
        }

        public void StageSelectYes()
        {
            Debug.Log($"스테이지 선택 예 클릭");
            m_stage_select_check_UI.SetActive(false);

            SaveManager.Instance.Player.m_stage_id = m_current_index;
            SaveManager.Instance.Player.m_stage_state = 0;

            m_talk_manager.ChangeTalkScene();
        }

        public void SelectButtonInteract()
        {
            for(int i = 1; i <= SaveManager.Instance.Player.m_max_clear_stage + 1; i++)
            {
                if(i > m_max_stage)
                {
                    Debug.Log($"더 활성화할 버튼이 없음");
                    return;
                }
                m_select_buttons[i].interactable = true;
                m_select_buttons[i].GetComponent<Image>().sprite = m_stage_status_images[(int)StageButton.UNLOCK];
            }
            Debug.Log($"스테이지 {SaveManager.Instance.Player.m_max_clear_stage + 1} 까지 버튼 활성화");
        }

        public void SelectButtonReset()
        {
            for(int i = 1; i <= (m_select_buttons.Length - 1); i++)
            {
                m_select_buttons[i].GetComponent<Image>().sprite = m_stage_status_images[(int)StageButton.LOCK];
                m_select_buttons[i].interactable = false;
            }
        }

        public void ClickedButtonIndex(int button_index) //클릭된 버튼의 index값을 불러와서 지금 위치와 가려는 위치를 비교하여 정방향/역방향 경로를 리스트에 추가
        {
            Debug.Log($"ClickedButtonPosiotion 호출됨: button_index={button_index}, m_now_button_index={m_now_button_index}");
            if (button_index < 0 || button_index >= m_select_buttons.Length)
            {
                Debug.LogError($"잘못된 button_index: {button_index}, 유효 범위: 0 ~ {m_select_buttons.Length - 1}");
                return;
            }

            m_path_list.Clear();
            m_current_path_index = 0; 

            if (m_now_button_index < button_index) // 지금 위치보다 클릭한 버튼이 뒤면 정방향
            {
                for (int i = m_now_button_index + 1; i <= button_index; i++)
                {
                    Debug.Log($"m_path_list 추가: {i}");
                    m_path_list.Add(i);
                }
            }
            else if (m_now_button_index > button_index) // 역방향
            {
                for (int i = m_now_button_index - 1; i >= button_index; i--)
                {
                    Debug.Log($"m_path_list 추가: {i}");
                    m_path_list.Add(i);
                }
            }
            else
            {
                Debug.Log("같은 버튼 클릭: m_now_button_index 추가");
                m_path_list.Add(m_now_button_index); // 같은 버튼을 클릭한 경우 현재 위치 index를 넘겨줘서 그자리에 있도록
            }
            m_is_icon_moving = true;

            m_now_button_index = button_index; // 경로는 만들어졌으니 now_button_index 를 목표 버튼의 index로 초기화
            Debug.Log($"경로 생성 완료: {string.Join(", ", m_path_list)}");

            StartCoroutine(MoveIconCorutine());
        }
    }
}