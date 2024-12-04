using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Jongmin
{
    public class SaveManager : MonoBehaviour
    {
        private PlayerData m_now_player;

        public PlayerData Player
        {
            get { return m_now_player; }
        }
        
        private string m_save_path;

        [Header("Charcater Default Status")]
        [SerializeField]
        private List<CharacterStatus> m_character_statuses;

        public List<CharacterStatus> CharacterStatuses
        {
            get { return m_character_statuses;} 
        }

        private void Start()
        {
            m_save_path = Application.persistentDataPath + "/PlayerData.json";

            Debug.Log($"{m_save_path} 위치로 플레이어 데이터 저장 경로를 설정하였습니다.");

            if(File.Exists(m_save_path))
            {
                LoadData();
            }
            else
            {
                switch(GameManager.Instance.CharacterType)
                {
                case Character.SOCIA:
                    m_now_player = new PlayerData(GameManager.Instance.CharacterType, m_character_statuses[Convert.ToInt32(Character.SOCIA)]);
                    break;

                case Character.GOV:
                    m_now_player = new PlayerData(GameManager.Instance.CharacterType, m_character_statuses[Convert.ToInt32(Character.GOV)]);
                    break;

                case Character.ENVA:
                    m_now_player = new PlayerData(GameManager.Instance.CharacterType, m_character_statuses[Convert.ToInt32(Character.ENVA)]);
                    break;
                }

                SaveData();
            }

            gameObject.GetComponent<PlayerFactory>().InstantiatePlayer();
        }

        public void SaveData()
        {
            string data = JsonUtility.ToJson(m_now_player);
            File.WriteAllText(m_save_path, data);

            Debug.Log($"{m_save_path} 경로로 플레이어 데이터를 저장하는 데 성공하였습니다.");
        }

        public void LoadData()
        {
            string data = File.ReadAllText(m_save_path);
            m_now_player = JsonUtility.FromJson<PlayerData>(data);

            GameManager.Instance.CharacterType = m_now_player.m_character_type;

            Debug.Log($"{m_save_path} 경로에서 플레이어 데이터를 불러오는 데 성공하였습니다.");
        }
    }
}
