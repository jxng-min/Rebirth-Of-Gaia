using Jongmin;
using UnityEngine;
using UnityEngine.UI;

public class GraphCtrl : MonoBehaviour
{
    [SerializeField]
    private Slider m_strength_slider;
    [SerializeField]
    private Slider m_intellect_slider;
    [SerializeField]
    private Slider m_sociality_slider;
    [SerializeField]
    private Slider m_stamina_slider;
    [SerializeField]
    private Slider m_defense_slider;

    private void Update()
    {
        m_strength_slider.value = SaveManager.Instance.Player.m_player_status.m_strength / (SaveManager.Instance.Player.m_player_status.m_stamina / 5);
        m_intellect_slider.value =  SaveManager.Instance.Player.m_player_status.m_intellect / (SaveManager.Instance.Player.m_player_status.m_stamina / 5);
        m_sociality_slider.value = SaveManager.Instance.Player.m_player_status.m_sociality / (SaveManager.Instance.Player.m_player_status.m_stamina / 5);
        m_stamina_slider.value = SaveManager.Instance.Player.m_player_status.m_stamina / SaveManager.Instance.Player.m_player_status.m_stamina;
        m_defense_slider.value = SaveManager.Instance.Player.m_player_status.m_defense / (SaveManager.Instance.Player.m_player_status.m_stamina / 5);
    }

}
