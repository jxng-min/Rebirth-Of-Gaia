using UnityEngine;

public class InventoryManager:MonoBehaviour
{
    // 인벤토리작업중인지 여부
    public static bool m_inventory_activated = false;

    [SerializeField]
    private GameObject m_slot_grid;
    private SlotData[] m_slots;

    void Start()
    {
        m_slots = m_slot_grid.GetComponentsInChildren<SlotData>();
    }

    public void AcquireItem(ItemData item, int count = 1)
    {
        if(item.m_item_type != ItemData.ItemType.Equipment)
        {
            for(int i = 0; i < m_slots.Length; i++)
            {
                if(m_slots[i].m_item != null)
                {
                    if(m_slots[i].m_item.m_item_name == item.m_item_name)
                    {
                        m_slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }
        }
        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].m_item == null)
            {
                m_slots[i].AddItem(item, count);
                return;
            }
        }
    }
}
