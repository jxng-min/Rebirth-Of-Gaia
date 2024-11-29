using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class ItemData : ScriptableObject
{
    public string m_item_name;
    public ItemType m_item_type;
    public Sprite m_item_image;
    public GameObject m_item_prefab;
    public string m_equipment_type;

    public enum ItemType{
        Equipment,
        Resource,
        ETC
    }
}
