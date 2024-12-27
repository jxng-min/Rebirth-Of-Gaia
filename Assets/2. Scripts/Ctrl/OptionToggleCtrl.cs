using Jongmin;
using System;
using UnityEngine;
using UnityEngine.UI;
public class OptionToggleCtrl : MonoBehaviour
{
    [SerializeField]
    private Toggle m_toggle;
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private GameObject m_checkmark;
    private void Start()
    {
        if (m_toggle != null)
        {
            m_toggle.onValueChanged.AddListener(delegate { ToggleOnOff(); });
            Debug.Log("$Toggle 이벤트 연결 성공");
        }
        else
        {
            Debug.Log("$Toggle 연결 실패");
        }
    }
    public void ToggleOnOff()
    {
        BackgroundOnOff();
        CheckmarkOnOff();
        Debug.Log("$toggle onoff 실행 성공");
    }
    private void BackgroundOnOff()
    {
        if(m_background != null)
        {
            Debug.Log("$background인식 성공");
            Color currentcolor = m_background.color;
            if (currentcolor.a == 1f)
            {
                currentcolor.a = 0.12f;
                Debug.Log("$background alpha값 하락 성공");
            }
            else if (currentcolor.a == 0.12f)
            {
                currentcolor.a = 1f;
                Debug.Log("$background alpha값 상승 성공");
            }
            m_background.color = currentcolor;
        }
        else
        {
            Debug.Log("$background인식 실패");
        }
    }
    private void CheckmarkOnOff()
{
    if (m_checkmark != null)
    {
        Debug.Log("Checkmark 인식 성공");
        RectTransform rectTransform = m_checkmark.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 currentPosition = rectTransform.anchoredPosition;
            Debug.Log($"현재 위치: {currentPosition}");
            currentPosition.x *= -1;
            rectTransform.anchoredPosition = currentPosition;
            Debug.Log($"변경된 위치: {rectTransform.anchoredPosition}");
            Debug.Log("Checkmark 위치 변경 성공");
        }
    }
    else
    {
        Debug.Log("Checkmark 인식 실패");
    }
}
    
}