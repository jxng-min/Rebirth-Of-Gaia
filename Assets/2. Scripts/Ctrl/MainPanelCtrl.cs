using Jongmin;
using UnityEngine;

public class MainPanelCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject m_main_panel;

    [SerializeField]
    private GameObject m_clear_panel;

    [SerializeField]
    private GameObject m_dead_panel;

    public void ClearToMain()
    {
        m_main_panel.SetActive(true);
        m_clear_panel.SetActive(false);

        GameEventBus.Publish(GameEventType.SETTING);
    }

    public void DeadToMain()
    {
        m_main_panel.SetActive(true);
        m_dead_panel.SetActive(false);

        GameEventBus.Publish(GameEventType.SETTING);
    }
}
