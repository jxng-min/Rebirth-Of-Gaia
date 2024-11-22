using UnityEngine;
using Junyoung;

namespace Jongmin
{
    public class GovCtrl : PlayerCtrl
    {
        public override void SetPlayerSkill()
        {
            m_player_skills[0] = new GovSkill1();
            m_player_skills[0] = new GovSkill2();
            m_player_skills[0] = new GovSkill3();
        }
    }
}

