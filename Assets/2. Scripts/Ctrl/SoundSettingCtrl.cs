using Jongmin;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingCtrl : MonoBehaviour
{
    [Header("Save Manager")]
    [SerializeField]
    private SaveManager m_save_manager;

    [Header("Sound Setting UI")]
    [SerializeField]
    private Slider m_bgm_slider;

    [SerializeField]
    private Slider m_effect_slider;

    private void Start()
    {
        m_bgm_slider.value = SoundManager.Instance.BgmVolume;
        m_effect_slider.value = SoundManager.Instance.EffectVolume;

        m_bgm_slider.onValueChanged.AddListener(OnBgmVolumeChanged);
        m_effect_slider.onValueChanged.AddListener(OnEffectVolumeChanged);
    }

    private void OnBgmVolumeChanged(float value)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.BgmVolume = value;

            if(m_save_manager.Player == null)
            {
                Debug.Log("사운드의 볼륨을 참조할 플레이어가 null입니다.");
                return;
            }

            m_save_manager.Player.m_bgm_volume = value;
        }
        else
        {
            Debug.LogError("SoundManager가 null이라 백그라운드 볼륨을 조절할 수 없습니다.");
        }
    }

    private void OnEffectVolumeChanged(float value)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.EffectVolume = value;

            if(m_save_manager.Player == null)
            {
                Debug.Log("사운드의 볼륨을 참조할 플레이어가 null입니다.");
                return;
            }
            
            m_save_manager.Player.m_effect_volume = value;
        }
        else
        {
            Debug.LogError("SoundManager가 null이라 이펙트 볼륨을 조절할 수 없습니다.");
        }
    }
}
