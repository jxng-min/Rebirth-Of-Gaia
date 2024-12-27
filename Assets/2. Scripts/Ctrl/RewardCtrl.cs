using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Jongmin;
using UnityEngine.Networking;

namespace Taekyung
{
public class RewardCtrl : MonoBehaviour
    {
    [SerializeField]
    private Button m_reward_button;

    private List<RewardData> m_reward_data;

    private void Start()
    {
        LoadRewardData("RewardData.json");
    }

    private void LoadRewardData(string file_name)
    {
        
#if UNITY_EDITOR
            string file_path = Path.Combine(Application.streamingAssetsPath, file_name);

            if (!File.Exists(file_path))
            {
                return;
            }

            string json_data = File.ReadAllText(file_path);

            RewardDataWrapper wrapper = JsonUtility.FromJson<RewardDataWrapper>(json_data);

            if (wrapper == null || wrapper.RewardData == null)
            {
                return;
            }

            m_reward_data = new List<RewardData>(wrapper.RewardData);
#elif UNITY_ANDROID
            string json_path = $"jar:file://{Application.dataPath}!/assets/{file_name}";

            UnityWebRequest request = UnityWebRequest.Get(json_path);
            request.SendWebRequest();

            while(!request.isDone) {}
            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"파일 다운로드 실패: {request.error}");
                return;
            }

            var save_path = $"{Application.persistentDataPath}/RewardData.json";
            File.WriteAllBytes(save_path, request.downloadHandler.data);

            StreamReader reader = new StreamReader(save_path);
            string json_content = reader.ReadToEnd();
            reader.Close();

            RewardDataWrapper and_wrapper = JsonUtility.FromJson<RewardDataWrapper>(json_content);

            if (and_wrapper == null || and_wrapper.RewardData == null)
            {
                return;
            }

            m_reward_data = new List<RewardData>(and_wrapper.RewardData);
#endif
    }
    public void StageClear()
    {
        Debug.Log($"클리어 전 레벨 = {SaveManager.Instance.Player.m_player_status.m_current_level}, 클리어 전 경험치 = {SaveManager.Instance.Player.m_player_status.m_current_exp}");
        SaveManager.Instance.Player.m_player_status.m_current_exp += m_reward_data[SaveManager.Instance.Player.m_stage_id].m_exp;
        Debug.Log($"클리어 후 레벨 = {SaveManager.Instance.Player.m_player_status.m_current_level}, 클리어 후 경험치 = {SaveManager.Instance.Player.m_player_status.m_current_exp}");
    }
    }
}