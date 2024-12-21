using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform m_rect;

    [Header("Handle")]
    [SerializeField] 
    private RectTransform m_handle;

    private Vector2 m_touch = Vector2.zero;
    private float m_width_half;

    [Header("Value")]
    [SerializeField] 
    private JoyStickValue m_value;

    [Header("Arrow")]
    [SerializeField]
    private GameObject m_arrow;

    [SerializeField]
    private GameObject m_light;

    private void Start()
    {
        m_rect = GetComponent<RectTransform>();
        m_width_half = m_rect.sizeDelta.x * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_arrow.SetActive(true);
        m_light.SetActive(true);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rect, eventData.position, eventData.pressEventCamera, out localPoint);
        m_touch = localPoint / m_width_half;

        if(m_touch.magnitude > 0.55f)
        {
            m_touch = m_touch.normalized * 0.55f;
        }
        m_value.m_joy_touch = m_touch;

        float angle = Mathf.Atan2(m_touch.y, m_touch.x) * Mathf.Rad2Deg;
        m_arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        m_light.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        m_arrow.GetComponent<RectTransform>().anchoredPosition = m_touch.normalized * 0.9f * m_width_half;
        m_light.GetComponent<RectTransform>().anchoredPosition = m_touch.normalized * 0.7f * m_width_half;
        m_handle.anchoredPosition = m_touch * m_width_half;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_value.m_joy_touch = Vector2.zero;
        m_handle.anchoredPosition = Vector2.zero;

        m_arrow.SetActive(false);
        m_light.SetActive(false);
    }
}