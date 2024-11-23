namespace Jongmin
{
    [System.Serializable]
    public class PlayerStatus
    {
        public float m_attack_power;
        public float m_magic_power;
        public float m_social_influence;

        public int m_ap_enforce;
        public int m_mp_enforce;
        public int m_si_enforce;
        public int m_stat_token;

        public int m_current_level;
        public int m_current_exp;


        public PlayerStatus(float attack_power, float magic_power, float social_influence)
        {
            m_attack_power = attack_power;
            m_magic_power = magic_power;
            m_social_influence = social_influence;

            m_ap_enforce = 0;
            m_mp_enforce = 0;
            m_si_enforce = 0;
            m_stat_token = 0;

            m_current_level = 1;
            m_current_exp = ExpData.m_exps[m_current_level - 1];
        }
    }
}