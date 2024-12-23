using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Jongmin;

public class PlayStatusCtrl : MonoBehaviour
{
    [Header("Character")]
    [SerializeField]
    private GameObject[] m_character_portrait;
    [SerializeField]
    private TMP_Text m_character_name_text;

    [Header("Stamina")]
    [SerializeField]
    private Slider m_stamina_slider;
    [SerializeField]
    private TMP_Text m_player_stamina_text;

    [Header("Level")]
    [SerializeField]
    private Slider m_exp_slider;
    [SerializeField]
    private TMP_Text m_level_text;

    private void Start()
    {
        for(int i = 0; i < m_character_portrait.Length; i++)
        {
            if(i == (int)GameManager.Instance.CharacterType)
            {
                m_character_portrait[i].SetActive(true);
            }
            else
            {
                m_character_portrait[i].SetActive(false);
            }
        }
        m_character_name_text.text = GameManager.Instance.CharacterType.ToString();
    }

    void Update()
    {
        m_stamina_slider.value = SaveManager.Instance.Player.m_player_status.m_stamina;
        m_player_stamina_text.text = $"{SaveManager.Instance.Player.m_player_status.m_stamina} / {SaveManager.Instance.Player.m_player_status.m_max_stamina}";

        m_exp_slider.value = SaveManager.Instance.Player.m_player_status.m_current_exp / ExpData.m_exps[SaveManager.Instance.Player.m_player_status.m_current_level - 1];
        m_level_text.text = SaveManager.Instance.Player.m_player_status.m_current_level.ToString();
    }
}
