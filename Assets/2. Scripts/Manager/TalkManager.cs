using System.Collections.Generic;
using Jongmin;
using Junyoung;
using UnityEngine;

namespace Taekyung
{

    public class TalkManager : MonoBehaviour
    {
        private Dictionary<string, string[]> m_talk_data;
        private Dictionary<int, Sprite> m_portrait_data;

        [Header("Talk UI")]
        [SerializeField]
        private TalkUIManager m_talk_ui_manager;

        [SerializeField]
        private Sprite[] m_portrait_arr;
        [SerializeField]
        private GameObject m_narration_panel;

        [Header("Manager")]
        [SerializeField]
        private StageManager m_stage_manager;

        [SerializeField]
        private GameObject m_main_panel;

        [SerializeField]
        private TypingEffectCtrl m_talk_effect;
        private bool m_is_action;
        private string m_save_path;
        private string m_current_talk;

        private void Start()
        {
            m_save_path = Application.streamingAssetsPath;
            m_portrait_data = new Dictionary<int, Sprite>();

            GeneratePortrait();
            BringTalkLineDataFromJson();

            //Talkking 이벤트 구독
            GameEventBus.Subscribe(GameEventType.TALKING, ChangeTalkScene);
        }

        private void OnDestroy()
        {
            GameEventBus.Unsubscribe(GameEventType.TALKING, ChangeTalkScene);
        }
        // 대화 중 초상화 UI를 생성하기 위한 메소드
        public void GeneratePortrait()
        {
            for(int i = 0; i < m_portrait_arr.Length; i++)
            {
                m_portrait_data.Add(i , m_portrait_arr[i]);

            }
        }

        // JSON 파일에서 NPC 정보와 대사를 불러오는 메소드
        public void BringTalkLineDataFromJson()
        {
            string json_path = m_save_path + "/TalkData.json";

            if (!System.IO.File.Exists(json_path))
            {
                Debug.Log($"{m_save_path}에 일치하는 TalkData.json이 없습니다.");
                return;
            }

            string json_string = System.IO.File.ReadAllText(json_path);
            TalkDataWrapper talk_data_wrapper = JsonUtility.FromJson<TalkDataWrapper>(json_string);

            if (talk_data_wrapper == null || talk_data_wrapper.m_talk_datas == null)
            {
                Debug.Log($"{m_save_path}/TalkData.json에서 NPC 정보와 대사를 불러오는 데 실패하였습니다.");
                return;
            }
            else
            {
                Debug.Log($"{m_save_path}/TalkData.json에서 NPC 정보와 대사를 불러오는 데 성공하였습니다.");
            }

            m_talk_data = new Dictionary<string, string[]>();
            foreach (var data in talk_data_wrapper.m_talk_datas)
            {
                if (data.m_stage_data == null)
                    continue;

                m_talk_data[data.m_stage_data] = data.m_talk_data;
            }
        }
        // scene_name과 일치하는 scene의 대사를 state에 맞게 리턴하는 메소드
        public string GetTalkData(string scene_name, int talk_idx)
        {
            foreach (var key in m_talk_data.Keys)
            {
                if (key == scene_name)
                {
                    if (talk_idx >= m_talk_data[key].Length)
                    {
                        return null;
                    }

                    return m_talk_data[key][talk_idx];
                }
            }

            return null;
        }

        // id와 일치하는 NPC의 스프라이트를 리턴하는 메소드
        public Sprite GetPortrait(int portrait_idx)
        {
            foreach (var key in m_portrait_data.Keys)
            {
                if (key == portrait_idx)
                {
                    return m_portrait_data[key];
                }
            }
            return null;
        }

        // 상호작용 메소드
        public void ChangeTalkScene()
        {
            SaveManager.Instance.Player.m_talk_state = true;
            Talk(SaveManager.Instance.Player.m_stage_id);
            m_talk_ui_manager.SetTalkUIActive(m_is_action);
            m_main_panel.SetActive(false);
        }

        // 대화를 진행하는 메소드
        public void Talk(int stage_id)
        {
            if (m_talk_effect.CurrentState() == true)
            {
                Debug.Log($"대화중");
                m_talk_effect.SetTextHard(m_current_talk);
                return;
            }
            // Set Talk Data
            string talk_data;
            talk_data = GetTalkData(stage_id + "_" + SaveManager.Instance.Player.m_stage_state, SaveManager.Instance.Player.m_talk_idx);
            // End Talk 
            if (talk_data == null)
            {
                m_is_action = false;

                SaveManager.Instance.Player.m_talk_idx = 0;
                SaveManager.Instance.Player.m_stage_state += 1;
                // m_stage_state 가 1 이 라는 것이 스테이지 시작 화면이라는 것을 뜻함
                if(SaveManager.Instance.Player.m_stage_state == 1)
                {
                    m_narration_panel.SetActive(false);
                    m_stage_manager.LoadStage(stage_id);
                }
                
                // talk 상태가 아님을 알림
                SaveManager.Instance.Player.m_talk_state = false;
                return;
            }
 
            // : 이후 숫자에 따른 초상화 선택 및 대사 선택
            string[] split_data = talk_data.Split(';');
            for(int i = 0; i < split_data.Length; i++)
            {
                Debug.Log(split_data[i]);
            }
            m_talk_effect.SetText(split_data[0]);
            string portrait_index = split_data.Length > 1 ? split_data[1] : "0";

            // 플레이어의 대사 차례인지 확인
            bool is_player;
            if (split_data.Length > 2)
            {
                is_player = true;
            }
            else
            {
                is_player = false;
            }

            // 초상화 가져오기
            Sprite portrait = GetPortrait(int.Parse(portrait_index));
            
            // ui 변경
            m_talk_ui_manager.UpdateTalkUI(portrait, is_player);

            m_is_action = true;
            SaveManager.Instance.Player.m_talk_idx++;
            m_current_talk = split_data[0];
        }
    }
}