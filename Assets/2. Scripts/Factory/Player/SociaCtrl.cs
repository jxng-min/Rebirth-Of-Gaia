using UnityEngine;
using Junyoung;

namespace Junyoung
{
    public class SociaCtrl : PlayerCtrl
    {
        [Header("Skill2 Area")]
        [SerializeField]
        Vector2 m_hit_box_size;
        [SerializeField]
        Vector2 m_hit_box_center;

        private bool m_was_flipX = false;

        public void Update()
        {
            if (GetComponent<SpriteRenderer>().flipX != m_was_flipX)
            {
                m_hit_box_center.x *= -1;
                m_was_flipX = GetComponent<SpriteRenderer>().flipX;
            }
        }

        public override void SetPlayerSkill()
        {
            m_player_skills[0] = gameObject.AddComponent<SociaSkill1>();
            m_player_skills[1] = gameObject.AddComponent<SociaSkill2>();
            m_player_skills[2] = gameObject.AddComponent<SociaSkill3>();
        }

        public override void PlayerUseSkill1()
        {
            m_player_skills[0].Effect();
        }
        public override void PlayerUseSkill2()
        {
            Collider2D[] in_box_colliders = Physics2D.OverlapBoxAll((Vector2)this.transform.position + m_hit_box_center, m_hit_box_size, 0);
            foreach (Collider2D in_collider in in_box_colliders)
            {
                if (in_collider.tag == "Enemy")
                {
                    SociaSkill2 skill = m_player_skills[1] as SociaSkill2;
                    skill.Effect(in_collider);
                }
                else
                {
                    Debug.Log($"사거리 내에 적이 존재하지 않음");
                }
            }
            
        }

        public override void PlayerUseSkill3()
        {
            m_player_skills[2].Effect();
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube((Vector2)this.transform.position +m_hit_box_center , m_hit_box_size);
        }

    }
}

