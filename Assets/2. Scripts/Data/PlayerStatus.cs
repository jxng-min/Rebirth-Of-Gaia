namespace Jongmin
{
    [System.Serializable]
    public class PlayerStatus
    {
        public float m_strength;
        public float m_intellect;
        public float m_sociality;

        public int m_ap_enforce;
        public int m_mp_enforce;
        public int m_si_enforce;
        public int m_stat_token;

        public int m_current_level;
        public int m_current_exp;


        public PlayerStatus(float attack_power, float magic_power, float social_influence)
        {
            m_strength = attack_power;
            m_intellect = magic_power;
            m_sociality = social_influence;

            m_ap_enforce = 0;
            m_mp_enforce = 0;
            m_si_enforce = 0;
            m_stat_token = 0;

            m_current_level = 1;
            m_current_exp = ExpData.m_exps[m_current_level - 1];
        }
    }
}