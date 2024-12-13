namespace Jongmin
{
    [System.Serializable]
    public class PlayerStatus
    {
        public float m_strength;
        public float m_intellect;
        public float m_sociality;

        public float m_stamina;
        public float m_defense;

        public int m_ap_enforce;
        public int m_mp_enforce;
        public int m_si_enforce;
        public int m_stat_token;

        public int m_current_level;
        public int m_current_exp;


        public PlayerStatus(CharacterStatus character_status)
        {
            m_strength = character_status.Strength;
            m_intellect = character_status.Intellect;
            m_sociality = character_status.Sociality;
            m_stamina = character_status.Stamina;
            m_defense = character_status.Defense;

            m_ap_enforce = 0;
            m_mp_enforce = 0;
            m_si_enforce = 0;
            m_stat_token = 0;

            m_current_level = 1;
            m_current_exp = 0;
        }
    }
}