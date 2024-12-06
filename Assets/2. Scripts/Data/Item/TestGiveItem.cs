using UnityEngine;

public class TestGiveItem : MonoBehaviour
{
    [SerializeField]
    private ItemData m_item;

    [SerializeField]
    private InventoryManager m_inventory;

    public void AcquireItem()
    {
        m_inventory.AcquireItem(m_item);
    }
}
