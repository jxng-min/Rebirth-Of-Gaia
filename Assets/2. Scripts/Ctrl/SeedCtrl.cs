using Jongmin;
using Junyoung;
using System.Collections;
using UnityEngine;

public class SeedCtrl : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_ground_layer;

    public ItemData m_seed_data;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (this.gameObject.layer == 14)
        {
            Debug.Log(coll.gameObject.layer + " " + m_ground_layer.value);
            if (((1 << coll.gameObject.layer) & m_ground_layer) != 0)
            {
                Debug.Log("희망의 씨앗이 자라납니다.");
                StartCoroutine(SetAlphaDown());
                StartCoroutine(Germination());
            }
        }
    }

    private IEnumerator SetAlphaDown()
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        Color color = sprite_renderer.color;

        float target_time = 1f;
        float elapsed_time = 0f;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;
            
            float alpha = Mathf.Lerp(1f, 0f, elapsed_time / target_time);
            sprite_renderer.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }
        sprite_renderer.color = new Color(color.r, color.g, color.b, 0f);
    }

    private IEnumerator Germination()
    {
        SoundManager.Instance.PlayEffect("seed_01");
        
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        Color color = sprite_renderer.color;

        GetComponent<Animator>().SetBool("IsDropped", true);

        float target_time = 10f;
        float elapsed_time = 0f;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, elapsed_time / target_time);
            sprite_renderer.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }
        sprite_renderer.color = new Color(color.r, color.g, color.b, 1f);

        GameEventBus.Publish(GameEventType.CLEAR);
    }
}
