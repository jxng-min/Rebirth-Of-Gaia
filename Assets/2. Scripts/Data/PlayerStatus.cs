[System.Serializable]
public class PlayerStatus
{
    public float m_attack_power;
    public float m_magic_power;
    public float m_social_influence;

    public PlayerStatus(float attack_power, float magic_power, float social_influence)
    {
        m_attack_power = attack_power;
        m_magic_power = magic_power;
        m_social_influence = social_influence;
    }
}
