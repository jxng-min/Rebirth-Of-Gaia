using UnityEngine;

[CreateAssetMenu(fileName = "Character Status", menuName = "Scriptable Object/Character Default Status", order = int.MaxValue)]
public class CharacterStatus : ScriptableObject
{
    [Header("Character Name")]
    [SerializeField]
    private string m_character_name;

    public string CharacterName
    {
        get { return m_character_name;}
    }

    [Header("Default Status")]
    [SerializeField]
    private float m_strength;

    public float Strength
    {
        get { return m_strength; }
    }

    [SerializeField]
    private float m_intellect;

    public float Intellect
    {
        get { return m_intellect;}
    }

    [SerializeField]
    private float m_sociality;

    public float Sociality
    {
        get { return m_sociality;}
    }
}
