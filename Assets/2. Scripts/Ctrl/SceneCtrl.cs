using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

namespace Jongmin
{
    public class SceneCtrl : MonoBehaviour
    {
        private static string m_scene_name;

        [SerializeField]
        private Image m_progress_bar;

        [SerializeField]
        private TMP_Text m_progress_text;

        private void Start()
        {
            StartCoroutine(LoadSceneProcess());
        }

        public static void ReplaceScene(string scene_name)
        {
            m_scene_name = scene_name;
            SceneManager.LoadScene("Loading");
        }

        private IEnumerator LoadSceneProcess()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(m_scene_name);
            op.allowSceneActivation = false;

            GameEventBus.Publish(GameEventType.LOADING);

            float timer = 0f;
            float display_progress = 0f;

            while(!op.isDone)
            {
                if(op.progress > 0.5f)
                {
                    timer += Time.unscaledDeltaTime;
                    display_progress = Mathf.Lerp(0.5f, 1f, timer / 2f);

                    if(timer >= 2f)
                    {
                        display_progress = 1f;
                        m_progress_text.text = "100%";
                        
                        yield return new WaitForSeconds(0.5f);

                        op.allowSceneActivation = true;

                        yield break;
                    }
                }
                else
                {
                    display_progress = Mathf.Clamp01(op.progress / 0.9f);
                }

                m_progress_text.text = Mathf.FloorToInt(display_progress * 100).ToString() + "%";
                m_progress_bar.transform.Rotate(0f, 0f, -1f);

                yield return null;
            }
        }
    }
}