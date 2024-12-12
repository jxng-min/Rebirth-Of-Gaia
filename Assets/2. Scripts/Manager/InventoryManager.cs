using Jongmin;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_slot_grid;

    [SerializeField]
    private SlotData[] m_slots;

    [SerializeField]
    private TMP_Text m_seeds_text;

    void Start()
    {
        //m_slots = m_slot_grid.GetComponentsInChildren<SlotData>();
    }

    public void AcquireItem(ItemData item, int count = 1)
    {
        if(item.ItemType != ItemType.Equipment)
        {
            for(int i = 0; i < m_slots.Length; i++)
            {
                if(m_slots[i].Item != null)
                {
                    if(m_slots[i].Item.ItemName == item.ItemName)
                    {
                        m_slots[i].SetSlotCount(count);

                        if(m_slots[i].Item.ItemName == "Seed of Desire")
                        {
                            m_seeds_text.text = m_slots[i].ItemCount.ToString();
                            Debug.Log($"씨앗을 습득 하였습니다.");
                        }

                        return;
                    }
                }
            }
        }

        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].Item == null)
            {
                m_slots[i].AddItem(item, count);
                return;
            }
        }
    }

    public SlotData SearchItem(string item_name)
    {
        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].Item?.ItemName == item_name)
            {
                return m_slots[i];
            }
        }

        return null;
    }

    public void OpenInventory()
    {
        GameEventBus.Publish(GameEventType.SETTING);
    }

    public void CloseInventory()
    {
        GameEventBus.Publish(GameEventType.PLAYING);
    }
}
