using UnityEngine;
using UnityEngine.UI;

public class SlotData : MonoBehaviour
{
    [Header("Item Data")]
    private ItemData m_item;
    public ItemData Item
    {
        get { return m_item; }
    }
    private int m_item_count;
    public int ItemCount
    {
        get { return m_item_count; }
        set { m_item_count = value; }
    }

    public Image m_item_image;
    public Image ItemImage
    {
        get { return m_item_image; }
    }

    [Header("Slot UI")]
    [SerializeField]
    private Text m_text_count;
    [SerializeField]
    private GameObject m_text_image;

    // 이미지의 투명도를 조절하는 메소드
    private void SetAlpha(float alpha)
    {
        var color = m_item_image.color;

        color.a = alpha;
        m_item_image.color = color;
    }

    // 아이템 획득을 처리하는 메소드
    public void AddItem(ItemData item, int count = 1)
    {
        m_item = item;
        m_item_count = count;
        m_item_image.sprite = item.ItemImage;

        if(m_item.ItemType != ItemType.Equipment)
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

    // 아이템 개수를 조정하는 메소드
    public void SetSlotCount(int count)
    {
        m_item_count += count;
        m_text_count.text = m_item_count.ToString();

        if(m_item_count <= 0)
        {
            ClearSlot();
        }
    }

    // 슬롯을 초기화하는 메소드
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
