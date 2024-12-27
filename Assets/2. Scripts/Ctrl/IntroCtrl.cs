using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

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
        private TMP_Text m_intro_text;
        private string[] m_intro_strings;

        [SerializeField]
        private Image m_seed_image;

        private void Awake()
        {
            m_intro_strings = new string[4]
            {
                "세상은 이미 한 번 죽음을 맞이했다.",
                "끝없이 이어진 지배층의 자원 착취, 환경 파괴와 대지의 황폐화…",
                "그러던 어느 날, 당신은 전설 속 상징인 희망의 씨앗에 대한 이야기를 듣게 된다.",
                "당신은 희망의 씨앗만 있다면 황폐화된 지구를  되돌릴 수 있다는 희망에 모험을 나선다."
            };

            m_intro_text.color = new Color(1f, 1f, 1f, 0f);
        }

        private void Start()
        {
            StartCoroutine(IntroStart());
            StartCoroutine(IntroTextStart(0));
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
        }

        private IEnumerator IntroTextStart(int index)
        {
            if(index == 0)
            {
                yield return new WaitForSeconds(1f);
            }
            
            float target_time = 2f;
            float elapsed_time = 0f;

            Color text_color = m_intro_text.color;
            m_intro_text.text = m_intro_strings[index];

            if(index == 3)
            {
                StartCoroutine(FadeOutBackground());
            }

            while(elapsed_time < target_time)
            {
                elapsed_time += Time.deltaTime;

                m_intro_text.color = new Color(text_color.r, text_color.g, text_color.b, Mathf.Lerp(0f, 1f, elapsed_time / target_time));

                yield return null;
            }

            yield return new WaitForSeconds(1f);
            
            elapsed_time = 0f;

            while(elapsed_time < target_time)
            {
                elapsed_time += Time.deltaTime;

                m_intro_text.color = new Color(text_color.r, text_color.g, text_color.b, Mathf.Lerp(1f, 0f, elapsed_time / target_time));

                yield return null;
            }

            if(index + 1 <= m_intro_strings.Length - 1)
            {
                StartCoroutine(IntroTextStart(index + 1));
            }
            else
            {
                StartCoroutine(PrintSeedImage());
                yield break;
            }
        }

        private IEnumerator PrintSeedImage()
        {
            float target_time = 2f;
            float elapsed_time = 0f;

            while(elapsed_time < target_time)
            {
                elapsed_time += Time.deltaTime;

                m_seed_image.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, elapsed_time / target_time));

                yield return null;
            }
            m_seed_image.color = new Color(1f, 1f, 1f, 1f);

            yield return new WaitForSeconds(1f);
            elapsed_time = 0f;

            while(elapsed_time < target_time)
            {
                elapsed_time += Time.deltaTime;

                m_seed_image.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, elapsed_time / target_time));

                yield return null;
            }
            m_seed_image.color = new Color(1f, 1f, 1f, 0f);

            SceneCtrl.ReplaceScene("PlayerSelect");
        }

        private IEnumerator FadeOutBackground()
        {
            float target_time = 5f;
            float elapsed_time = 0f;

            while(elapsed_time < target_time)
            {
                elapsed_time += Time.deltaTime;

                m_intro_object.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, Mathf.Lerp(0.75f, 0.1f, elapsed_time / target_time));

                yield return null;
            }
        }
    }
}
