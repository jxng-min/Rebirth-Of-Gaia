using System.Collections;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUICtrl : MonoBehaviour
{
    [SerializeField]
    private Scrollbar m_scroll_bar;

    [SerializeField]
    private GameObject[] m_panel_images;

    [SerializeField]
    private Transform[] m_diamond_contents;

    private Vector2[] m_diamond_pos;

    [SerializeField]
    private float m_swipe_time = 0.2f;

    [SerializeField]
    private float m_swipe_distance = 500.0f;

    private float[] m_scroll_page_values;
    private float m_value_distance = 0f;
    private int m_current_page = 0;
    private int m_max_page = 0;
    private float m_start_touch_x;
    private float m_end_touch_x;
    private bool m_is_swipe_mode = false;
    private float m_diamond_content_scale = 2f;
    private int m_previous_highlighted_index = -1;

    private void Awake()
    {
        m_scroll_page_values = new float[transform.childCount];

        m_diamond_pos = new Vector2[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            m_diamond_pos[i] = m_diamond_contents[i].GetComponent<RectTransform>().anchoredPosition;
            m_panel_images[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.7f);
        }

        m_value_distance = 1f / (m_scroll_page_values.Length - 1f);

        for(int i = 0; i < m_scroll_page_values.Length; i++)
        {
            m_scroll_page_values[i] = m_value_distance * i;
        }

        m_max_page = transform.childCount;

        StartCoroutine(AlphaChange(m_current_page, 0f));
    }

    private void Update()
    {
        UpdateInput();
        UpdateCircleContent();
    }

    private void OnEnable()
    {
        SetScrollBarValue(0);
    }

    private void Start()
    {
        SetScrollBarValue(0);
    }

    public void SetScrollBarValue(int index)
    {
        m_current_page = index;
        m_scroll_bar.value = m_scroll_page_values[index];
    }

    private IEnumerator AlphaChange(int index, float target_alpha)
    {
        float target_time = 0.5f;
        float elapsed_time = 0f;

        Color current_color = m_panel_images[index].GetComponent<Image>().color;
        float start_alpha = current_color.a;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            float new_alpha = Mathf.Lerp(start_alpha, target_alpha, elapsed_time / target_time);
            m_panel_images[index].GetComponent<Image>().color = new Color(current_color.r, current_color.g, current_color.b, new_alpha);

            yield return null;
        }

        m_panel_images[index].GetComponent<Image>().color = new Color(current_color.r, current_color.g, current_color.b, target_alpha);
    }

    public void UpdateInput()
    {
        if(m_is_swipe_mode)
        {
            return;
        }

        #if UNITY_ANDROID
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                m_start_touch_x = touch.position.x;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                m_end_touch_x = touch.position.x;

                UpdateSwipe();
            }
        }
        #endif
    }

    public void UpdateSwipe()
    {
        if(Mathf.Abs(m_start_touch_x - m_end_touch_x) < m_swipe_distance)
        {
            StartCoroutine(OnSwipeOneStep(m_current_page));
            return;
        }

        bool is_left = m_start_touch_x < m_end_touch_x ? true : false;

        if(is_left)
        {
            if(m_current_page == 0)
            {
                return;
            }

            StartCoroutine(AlphaChange(m_current_page, 0.7f));
            m_current_page--;
        }
        else
        {
            if(m_current_page == m_max_page - 1)
            {
                return;
            }

            StartCoroutine(AlphaChange(m_current_page, 0.7f));
            m_current_page++;
        }

        StartCoroutine(OnSwipeOneStep(m_current_page));
    }

    private IEnumerator OnSwipeOneStep(int index)
    {
        StartCoroutine(AlphaChange(m_current_page, 0.0f));

        float start = m_scroll_bar.value;
        float current = 0f;
        float percent = 0f;

        m_is_swipe_mode = true;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / m_swipe_time;

            m_scroll_bar.value = Mathf.Lerp(start, m_scroll_page_values[index], percent);

            yield return null;
        }

        m_is_swipe_mode = false;
    }

    private void UpdateCircleContent()
    {
        for (int i = 0; i < m_scroll_page_values.Length; i++)
        {
            m_diamond_contents[i].localScale = Vector2.one;
            m_diamond_contents[i].GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1f);

            if (m_previous_highlighted_index >= 0 && m_previous_highlighted_index != i)
            {
                m_diamond_contents[i].GetComponent<RectTransform>().anchoredPosition = m_diamond_pos[i];
            }

            if (m_scroll_bar.value < m_scroll_page_values[i] + (m_value_distance / 2) &&
                m_scroll_bar.value > m_scroll_page_values[i] - (m_value_distance / 2))
            {
                m_diamond_contents[i].localScale = Vector2.one * m_diamond_content_scale;
                m_diamond_contents[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                if (m_previous_highlighted_index != i)
                {
                    if (m_previous_highlighted_index >= 0)
                    {
                        m_diamond_contents[m_previous_highlighted_index].GetComponent<RectTransform>().anchoredPosition =
                            m_diamond_pos[m_previous_highlighted_index];
                    }

                    m_diamond_contents[i].GetComponent<RectTransform>().anchoredPosition =
                        m_diamond_pos[i] + Vector2.up * 20;

                    m_previous_highlighted_index = i;
                }
            }
        }
    }
}
