using UnityEngine;
using Junyoung;

namespace Jongmin
{
    public class EnvaCtrl : PlayerCtrl
    {
        public override void SetPlayerSkill()
        {
            m_player_skills[0] = new EnvaSkill1();
            m_player_skills[1] = new EnvaSkill2();
            m_player_skills[2] = new EnvaSkill3();
        }

        public override void PlayerAttack()
        {
            
        }
    }
}

