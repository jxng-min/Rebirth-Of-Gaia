using UnityEngine;
using Junyoung;
using System.Collections;
using Unity.VisualScripting;

public class SeedShortcutCtrl : MonoBehaviour
{
    private SlotData m_seed_data;
    private GameObject m_seed;
    private PlayerCtrl m_player_ctrl;

    [SerializeField]
    private GameObject m_seed_prefab;

    public void OnClick()
    {
        //CheckSeed();
    }

    public void CheckSeed()
    {
        if(!m_seed_data)
        {
            InventoryManager inventory_manager = FindAnyObjectByType<InventoryManager>();
            m_seed_data = inventory_manager.SearchItem("Seed of Desire");

            if(!m_seed_data)
            {
                return;
            }
        }

        PutSeed();
    }

    public void PutSeed()//버튼 클릭으로 씨앗 심기
    {
        if(m_seed_data.ItemCount <= 0)
        {
            return;
        }

        if(!m_player_ctrl)
        {
            m_player_ctrl = FindAnyObjectByType<PlayerCtrl>();
        }

        m_seed_data.ItemCount--;

        SpawnSeed(m_player_ctrl.transform.position);
    }

    public void SpawnSeed(Vector3 spawn_pos )//몬스터 처치시 씨앗 드랍
    {
        m_seed = Instantiate(m_seed_prefab, spawn_pos + Vector3.up * 2f, Quaternion.identity);

        StartCoroutine(ColliderEnabled());

        m_seed.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
    }

    private IEnumerator ColliderEnabled()
    {
        BoxCollider2D box_collider = m_seed.GetComponent<BoxCollider2D>();

        box_collider.enabled = false;

        float target_time = 1f;
        float elapsed_time = 0f;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;
            
            yield return null;
        }

        box_collider.enabled = true;
    }
}
