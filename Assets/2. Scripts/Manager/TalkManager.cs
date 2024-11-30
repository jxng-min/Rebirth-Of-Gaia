using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Jongmin;
using Junyoung;
using UnityEngine;

namespace Taekyung
{

    public class TalkManager : MonoBehaviour
    {
        private Dictionary<string, string[]> m_talk_data;
        private Dictionary<string, Sprite> m_portrait_data;

        [Header("Talk UI")]
        [SerializeField]
        private TalkUIManager m_talk_ui_manager;

        [SerializeField]
        private Sprite[] m_portrait_arr;

        [Header("SaveManager")]
        [SerializeField]
        private SaveManager m_save_manager;

        private bool m_is_action;
        private string m_save_path;

        private void Start()
        {
            m_save_path = Application.persistentDataPath + "/Data";
            m_portrait_data = new Dictionary<string, Sprite>();
            Debug.Log($"Persistent Data Path: {Application.persistentDataPath}");

            GeneratePortrait();
            BringTalkLineDataFromJson();
        }

        // 대화 중 초상화 UI를 생성하기 위한 메소드
        public void GeneratePortrait()
        {
            m_portrait_data.Add("scene_name" + "0", m_portrait_arr[0]);
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
        public Sprite GetPortrait(int stage_id, int portrait_idx)
        {
            foreach (var key in m_portrait_data.Keys)
            {
                if (key == stage_id + "portrait_idx")
                {
                    return m_portrait_data[key];
                }
            }
            return null;
        }

        // 상호작용 메소드
        public void ChangeTalkScene()
        {
            Talk(m_save_manager.Player.m_stage_id);
            m_talk_ui_manager.SetTalkUIActive(m_is_action);
        }

        // 대화를 진행하는 메소드
        public void Talk(int stage_id)
        {
            // Set Talk Data
            string talk_data;
            talk_data = GetTalkData(stage_id + "_" + m_save_manager.Player.m_stage_state, m_save_manager.Player.m_talk_idx);
            // End Talk 
            if (talk_data == null)
            {
                m_is_action = false;

                m_save_manager.Player.m_talk_idx = 0;

                return;
            }

            // : 이후 숫자에 따른 초상화 선택 및 대사 선택
            string[] split_data = talk_data.Split(';');
            for(int i = 0; i < split_data.Length; i++)
            {
                Debug.Log(split_data[i]);
            }
            string text = split_data[0];
            int portrait_index = split_data.Length > 1 ? int.Parse(split_data[1]) : 0;

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
            Sprite portrait = GetPortrait(m_save_manager.Player.m_stage_id, portrait_index);
            
            // ui 변경
            m_talk_ui_manager.UpdateTalkUI(text, portrait, is_player);

            m_is_action = true;
            m_save_manager.Player.m_talk_idx++;
        }
    }
}