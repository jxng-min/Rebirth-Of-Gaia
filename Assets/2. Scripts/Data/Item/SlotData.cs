using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotData:MonoBehaviour
{
    public ItemData m_item; // 획득한 아이템
    public int m_item_count; // 획득한 아이템의 개수
    public UnityEngine.UI.Image m_item_image; // 아이템 이미지

    [SerializeField]
    private Text m_text_count;
    [SerializeField]
    private GameObject m_text_image;

    // 이미지 투명도 조절
    private void SetAlpha(float alpha)
    {
        Color color = m_item_image.color;
        color.a = alpha;
        m_item_image.color = color;
    }

    // 아이템 획득
    public void AddItem(ItemData item, int count = 1)
    {
        m_item = item;
        m_item_count = count;
        m_item_image.sprite = item.m_item_image;

        if(m_item.m_item_type != ItemData.ItemType.Equipment)
        {
            m_text_image.SetActive(true);
            m_text_count.text = m_item_count.ToString();
        }
        else
        {
            m_text_count.text = "0";
            m_text_image.SetActive(false);
        }

        SetAlpha(1);
    }

    // 아이템 개수 조정
    public void SetSlotCount(int count)
    {
        m_item_count += count;
        m_text_count.text = m_item_count.ToString();

        if(m_item_count <= 0)
        {
            ClearSlot();
        }
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        m_item = null;
        m_item_count = 0;
        m_item_image.sprite = null;
        SetAlpha(0);

        m_text_count.text = "0";
        m_text_image.SetActive(false);
    }
}
