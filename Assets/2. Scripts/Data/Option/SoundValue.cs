using Jongmin;
using UnityEngine;
using UnityEngine.UI;

public class SoundValue : MonoBehaviour
{
    [SerializeField]
    private Slider m_bgm_slider;
    [SerializeField]
    private Slider m_effect_slider;
    [SerializeField]
    private SaveManager m_save_manager;

    private void Start()
    {
        Invoke("LateStart", 0.1f);
    }
    private void LateStart()
    {
        if(SoundManager.Instance != null)
        {
            m_bgm_slider.value = SoundManager.Instance.BgmVolume;
            m_effect_slider.value = SoundManager.Instance.EffectVolume;
        }

        m_bgm_slider.onValueChanged.AddListener(OnBgmVolumeChanged);
        m_effect_slider.onValueChanged.AddListener(OnEffectVolumeChanged);
        
        Debug.Log($"bgm:{m_save_manager.Player.m_bgm_volume},effect:{m_save_manager.Player.m_effect_volume}");
        OnBgmVolumeChanged(m_save_manager.Player.m_bgm_volume);
        OnEffectVolumeChanged(m_save_manager.Player.m_effect_volume);
    }
    public void OnBgmVolumeChanged(float value)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.BgmVolume = value;
            if(m_save_manager.Player == null)
            {
                Debug.Log($"m_save_manager.Player is null");

            }
            m_save_manager.Player.m_bgm_volume = value;
            m_save_manager.SaveData();
            Debug.Log($"BgmVolume = {value}조절 완료");
        }
        else{
            Debug.Log($"BgmVolume = {value}조절 실패");
        }
    }
    public void OnEffectVolumeChanged(float value)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.EffectVolume = value;
            m_save_manager.Player.m_effect_volume = value;
            m_save_manager.SaveData();
            Debug.Log($"EffectVolume = {value}조절 완료");
        }
        else{
            Debug.Log($"EffectVolume = {value}조절 실패");
        }
    }
}


