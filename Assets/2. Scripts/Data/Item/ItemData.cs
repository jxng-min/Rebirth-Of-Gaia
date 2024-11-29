using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/item", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [Header("Item Name")]
    [SerializeField]
    private string m_item_name;
    public string ItemName
    {
        get { return m_item_name; }
    }

    [Header("Item Type")]
    [SerializeField]
    private ItemType m_item_type;
    public ItemType ItemType
    {
        get { return m_item_type; }
    }

    [Header("Item Image")]
    [SerializeField]
    private Sprite m_item_image;
    public Sprite ItemImage
    {
        get { return m_item_image; }
    }

    [Header("Item Prefab")]
    [SerializeField]
    private GameObject m_item_prefab;
    public GameObject ItemObject
    {
        get { return m_item_prefab; }
    }

    [Header("Equipment Type")]
    [SerializeField]
    private string m_equipment_type;
    public string EquipmentType
    {
        get { return m_equipment_type; }
    }
}
