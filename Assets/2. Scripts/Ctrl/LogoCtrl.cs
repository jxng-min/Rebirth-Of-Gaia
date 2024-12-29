using Jongmin;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoCtrl : MonoBehaviour
{
    [SerializeField]
    private Image m_title_image;

    private void Start()
    {
        StartCoroutine(FadeInLogo());
    }

    private IEnumerator FadeInLogo()
    {
        float target_time = 3f;
        float elapsed_time = 0f;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            m_title_image.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, elapsed_time / target_time));

            yield return null;
        }

        m_title_image.color = new Color(1f, 1f, 1f, 1f);
        SceneManager.LoadScene("Title");
    }
}
