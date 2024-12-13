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

        public override void PlayerAttack()
        {
            if(Input.GetButtonDown("Fire2"))
            {
                Debug.Log($"중첩 되는 중 {AttackStack}");
                AttackStack++;
                AttackTimer = InputDelay;
            }

            if(AttackTimer > 0f || AttackStack >= 3)
            {
                AttackTimer -= Time.deltaTime;

                if(AttackTimer <= 0f)
                {
                    m_player_state_context.Transition(m_attack_state);
                    AttackStack = 0;
                }
            }
        }
    }
}

