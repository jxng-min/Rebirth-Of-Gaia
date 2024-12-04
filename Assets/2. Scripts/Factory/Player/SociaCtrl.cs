using UnityEngine;
using Junyoung;

namespace Jongmin
{
    public class SociaCtrl : PlayerCtrl
    {
        public override void SetPlayerSkill()
        {
            m_player_skills[0] = new SociaSkill1();
            m_player_skills[1] = new SociaSkill2();
            m_player_skills[2] = new SociaSkill3();
        }
    }
}

