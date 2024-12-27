using Jongmin;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionToggleCtrl : MonoBehaviour
{
    [SerializeField]
    private Animator m_bgm_toggle_animator;

    [SerializeField]
    private Animator m_effect_toggle_animator;

    [SerializeField]
    private Slider m_bgm_slider;

    [SerializeField]
    private Slider m_effect_slider;

    private void Awake()
    {
        m_bgm_slider.interactable = SaveManager.Instance.Player.m_bgm_slider_on;
        m_effect_slider.interactable = SaveManager.Instance.Player.m_effect_slider_on;

        if(!SaveManager.Instance.Player.m_bgm_slider_on)
        {
            m_bgm_toggle_animator.SetTrigger("Disabled");
        }
        else
        {
            m_bgm_toggle_animator.ResetTrigger("Disabled");
        }

        if(!SaveManager.Instance.Player.m_effect_slider_on)
        {
            m_effect_toggle_animator.SetTrigger("Disabled");
        }
        else
        {
            m_effect_toggle_animator.ResetTrigger("Disabled");
        }
    }

    private void OnEnable()
    {
        m_bgm_slider.interactable = SaveManager.Instance.Player.m_bgm_slider_on;
        m_effect_slider.interactable = SaveManager.Instance.Player.m_effect_slider_on;

        if(!SaveManager.Instance.Player.m_bgm_slider_on)
        {
            m_bgm_toggle_animator.SetTrigger("Disabled");
        }
        else
        {
            m_bgm_toggle_animator.ResetTrigger("Disabled");
        }

        if(!SaveManager.Instance.Player.m_effect_slider_on)
        {
            m_effect_toggle_animator.SetTrigger("Disabled");
        }
        else
        {
            m_effect_toggle_animator.ResetTrigger("Disabled");
        }
    }

    private void Update()
    {
        if(!SaveManager.Instance.Player.m_bgm_slider_on)
        {
            m_bgm_toggle_animator.SetTrigger("Disabled");
        }
        else
        {
            m_bgm_toggle_animator.ResetTrigger("Disabled");
        }

        if(!SaveManager.Instance.Player.m_effect_slider_on)
        {
            m_effect_toggle_animator.SetTrigger("Disabled");
        }
        else
        {
            m_effect_toggle_animator.ResetTrigger("Disabled");
        }
    }

    public void BgmToggle()
    {
        if(SaveManager.Instance.Player.m_bgm_slider_on)
        {
            SaveManager.Instance.Player.m_bgm_slider_on = false;
        }
        else
        {
            SaveManager.Instance.Player.m_bgm_slider_on = true;
        }
    }

    public void EffectToggle()
    {
        if(SaveManager.Instance.Player.m_effect_slider_on)
        {
            SaveManager.Instance.Player.m_effect_slider_on = false;
        }
        else
        {
            SaveManager.Instance.Player.m_effect_slider_on = true;
        }
    }
}
