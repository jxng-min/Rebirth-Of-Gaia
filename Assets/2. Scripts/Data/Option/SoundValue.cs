using Jongmin;
using UnityEngine;
using UnityEngine.UI;

public class SoundValue : MonoBehaviour
{
    [SerializeField]
    private Slider m_bgm_slider;
    [SerializeField]
    private Slider m_effect_slider;

    private void start()
    {
        if(SoundManager.Instance != null)
        {
            m_bgm_slider.value = SoundManager.Instance.BgmVolume;
            m_effect_slider.value = SoundManager.Instance.EffectVolume;
        }

        m_bgm_slider.onValueChanged.AddListener(OnBgmVolumeChanged);
        m_effect_slider.onValueChanged.AddListener(OnEffectVolumeChanged);
    }
    public void OnBgmVolumeChanged(float value)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.BgmVolume = value;
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
            Debug.Log($"EffectVolume = {value}조절 완료");
        }
        else{
            Debug.Log($"EffectVolume = {value}조절 실패");
        }
    }
}


