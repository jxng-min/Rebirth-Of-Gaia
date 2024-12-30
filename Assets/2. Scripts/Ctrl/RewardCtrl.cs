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
        LoadRewardData();
    }

    private void LoadRewardData()
    {
            TextAsset textAsset = Resources.Load<TextAsset>("RewardData");
            string json_content = textAsset.text;

            RewardDataWrapper and_wrapper = JsonUtility.FromJson<RewardDataWrapper>(json_content);

            if (and_wrapper == null || and_wrapper.RewardData == null)
            {
                return;
            }

            m_reward_data = new List<RewardData>(and_wrapper.RewardData);
    }
    
    public void StageClear()
    {
        Debug.Log($"클리어 전 레벨 = {SaveManager.Instance.Player.m_player_status.m_current_level}, 클리어 전 경험치 = {SaveManager.Instance.Player.m_player_status.m_current_exp}");
        SaveManager.Instance.Player.m_player_status.m_current_exp += m_reward_data[SaveManager.Instance.Player.m_stage_id].m_exp;
        Debug.Log($"클리어 후 레벨 = {SaveManager.Instance.Player.m_player_status.m_current_level}, 클리어 후 경험치 = {SaveManager.Instance.Player.m_player_status.m_current_exp}");
    }
    }
}