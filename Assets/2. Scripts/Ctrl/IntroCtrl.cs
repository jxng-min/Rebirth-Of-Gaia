using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Jongmin
{
    public class IntroCtrl : MonoBehaviour
    {
        [Header("Intro Play")]
        [SerializeField]
        private float m_intro_speed;

        [SerializeField]
        private float m_intro_scroll_time;

        [Header("UI")]
        [SerializeField]
        private GameObject m_intro_object;

        [SerializeField]
        private Button m_next_button;

        private void Start()
        {
            m_next_button.interactable = false;
            StartCoroutine(IntroStart());
        }

        private IEnumerator IntroStart()
        {
            float elapsed_time = 0f;
            while(elapsed_time < m_intro_scroll_time)
            {
                m_intro_object.transform.Translate(Vector3.left * m_intro_speed * Time.deltaTime);
                elapsed_time += Time.deltaTime;

                yield return null;
            }

            m_next_button.interactable = true;
        }
    }
}
