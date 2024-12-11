using UnityEngine;
using UnityEngine.UI;

public class ToggleCtrl : MonoBehaviour
{
    [SerializeField]
    private Toggle m_target_toggle;

    [SerializeField]
    private ToggleGroup m_toggle_group;

    private void OnEnable()
    {
        if(m_target_toggle == null)
        {
            Debug.Log("상태를 설정할 토글이 null입니다.");
            return;
        }
        m_toggle_group.gameObject.SetActive(true);

        m_target_toggle.isOn = true;
        m_target_toggle.GetComponent<Animator>().SetTrigger("Selected");
    }

    private void OnDisable()
    {
        foreach (var toggle in m_toggle_group.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }           
    }

    private void Update()
    {
        foreach (var toggle in m_toggle_group.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(toggle, isOn));
        }        
    }

    private void OnToggleValueChanged(Toggle toggle, bool isOn)
    {
        if (toggle == m_target_toggle && !isOn)
        {
            TriggerAnimation(m_target_toggle, "Normal");
        }
        else if (isOn)
        {
            TriggerAnimation(toggle, "Selected");
        }
    }

    private void TriggerAnimation(Toggle toggle, string triggerName)
    {
        var animator = toggle.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
    }
}
