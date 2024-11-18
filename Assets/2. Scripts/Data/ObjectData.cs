using UnityEngine;

public class ObjectData: MonoBehaviour
{
    public int m_id;
    public bool m_is_npc;

    public ObjectData(int id, bool is_npc)
    {
        m_id = id;
        m_is_npc = is_npc;
    }
}