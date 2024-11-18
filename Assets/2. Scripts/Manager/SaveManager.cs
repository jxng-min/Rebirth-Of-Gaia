using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using System;

namespace Jongmin
{
    public class DataManager : MonoBehaviour
    {
        private PlayerData m_now_player = new PlayerData();
        private string m_save_path;

        private void Start()
        {
            m_save_path = Application.persistentDataPath + "/Save";

            Debug.Log($"{m_save_path} 위치로 플레이어 데이터 저장 경로를 설정하였습니다.");
        }

        public void SaveData()
        {
            string data = JsonUtility.ToJson(m_now_player);
            File.WriteAllText(m_save_path + "PlayerData", data);

            Debug.Log($"{m_save_path} 경로로 플레이어 데이터를 저장하는 데 성공하였습니다.");
        }

        public void LoadData()
        {
            string data = File.ReadAllText(m_save_path + "PlayerData");
            m_now_player = JsonUtility.FromJson<PlayerData>(data);

            Debug.Log($"{m_save_path} 경로에서 플레이어 데이터를 불러오는 데 성공하였습니다.");
        }

        public void DataClear()
        {
            m_now_player = new PlayerData();

            Debug.Log($"플레이어 데이터를 초기화하는 데 성공하였습니다.");
        }
    }
}
