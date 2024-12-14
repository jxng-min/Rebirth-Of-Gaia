using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUICtrl : MonoBehaviour
{
    [SerializeField]
    private Scrollbar m_scroll_bar;

    [SerializeField]
    private Transform[] m_diamond_contents;

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

    private void Awake()
    {
        m_scroll_page_values = new float[transform.childCount];

        m_value_distance = 1f / (m_scroll_page_values.Length - 1f);

        for(int i = 0; i < m_scroll_page_values.Length; i++)
        {
            m_scroll_page_values[i] = m_value_distance * i;
        }

        m_max_page = transform.childCount;
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

            m_current_page--;
        }
        else
        {
            if(m_current_page == m_max_page - 1)
            {
                return;
            }

            m_current_page++;
        }

        StartCoroutine(OnSwipeOneStep(m_current_page));
    }

    private IEnumerator OnSwipeOneStep(int index)
    {
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
        for(int i = 0; i < m_scroll_page_values.Length; i++)
        {
            m_diamond_contents[i].localScale = Vector2.one;
            m_diamond_contents[i].GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1f);

            if(m_scroll_bar.value < m_scroll_page_values[i] + (m_value_distance / 2) &&
               m_scroll_bar.value > m_scroll_page_values[i] - (m_value_distance / 2))
            {
                m_diamond_contents[i].localScale = Vector2.one * m_diamond_content_scale;
                m_diamond_contents[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
