using UnityEngine;
using Junyoung;
using System.Collections;
using Jongmin;

namespace Junyoung
{
    public class SociaCtrl : PlayerCtrl
    {
        [Header("Skill2 Area")]
        [SerializeField]
        Vector2 m_hit_box_size;
        [SerializeField]
        Vector2 m_hit_box_center;
        [SerializeField]
        private GameObject m_skill_effect;
        [SerializeField]
        private Vector2 m_skill_effect_center;

        //private bool m_was_flipX = false;

        public override void SetPlayerSkill()
        {
            m_player_skills[0] = gameObject.AddComponent<SociaSkill1>();
            m_player_skills[1] = gameObject.AddComponent<SociaSkill2>();
            m_player_skills[2] = gameObject.AddComponent<SociaSkill3>();
        }

        public override void PlayerUseSkill1()
        {
            SoundManager.Instance.PlayEffect("socia_skill_01");
            StartCoroutine(PlayEffect(3));
            m_player_skills[0].Effect();

        }

        public override void PlayerUseSkill2()
        {
            
            Collider2D[] in_box_colliders = Physics2D.OverlapBoxAll((Vector2)this.transform.position + new Vector2(m_hit_box_center.x * this.JoyStickDir, m_hit_box_center.y ), m_hit_box_size, 0);
            
            foreach (Collider2D in_collider in in_box_colliders)
            {
                if (in_collider.tag == "Enemy")
                {
                    SoundManager.Instance.PlayEffect("socia_skill_02");
                    (m_player_skills[1] as SociaSkill2).Effect(in_collider);
                    StartCoroutine(PlayEffect(2, in_collider.transform.position));
                    break;
                }
                else
                {
                    Debug.Log($"사거리 내에 적이 존재하지 않아 [설득의 힘]을 사용할 수 없습니다.");
                }
            }           
        }

        public override void PlayerUseSkill3()
        {
            StartCoroutine(PlayEffect(1));
            SoundManager.Instance.PlayEffect("socia_skill_03");
            m_player_skills[2].Effect();
        }

        public override void PlayerAttack()
        {
            Vector2 hitboxPosition = (Vector2)this.transform.position + new Vector2(m_hit_box_center.x * this.JoyStickDir, m_hit_box_center.y);
            Collider2D[] in_box_colliders = Physics2D.OverlapBoxAll(hitboxPosition, m_hit_box_size, 0);

            PlayerAttackState attackState = m_attack_state as PlayerAttackState;
            attackState.EnemyCollider = null;

            foreach (Collider2D in_collider in in_box_colliders)
            {
                Debug.Log($"Detected: {in_collider.name} with tag: {in_collider.tag}");
                if (in_collider.CompareTag("Enemy"))
                {
                    attackState.EnemyCollider = in_collider;
                    break;
                }
            }

            m_player_state_context.Transition(m_attack_state);
        }

        private IEnumerator PlayEffect(int skill_index)
        {
            var effect = Instantiate(m_skill_effect);
            effect.transform.position = (Vector2)this.transform.position + m_skill_effect_center;
            string trigger_name = $"Skill {skill_index}";
            effect.GetComponent<Animator>().SetTrigger(trigger_name);

            yield return new WaitForSeconds(1f);

            Destroy(effect);
        }

        private IEnumerator PlayEffect(int skill_index , Vector2 enemy_vector)
        {
            var effect = Instantiate(m_skill_effect);
            effect.transform.position = enemy_vector + m_skill_effect_center;
            string trigger_name = $"Skill {skill_index}";
            effect.GetComponent<Animator>().SetTrigger(trigger_name);

            yield return new WaitForSeconds(1f);

            Destroy(effect);
        }

        private IEnumerator DescendingAttackTimer()
        {
            yield return null;
            /*
            AttackTimer = InputDelay;
            
            Debug.Log($"중첩 되는 중 {AttackStack}");
            AttackStack++;

            while(AttackTimer > 0f)
            {
                AttackTimer -= Time.deltaTime;

                yield return null;
            }

            AttackTimer = 0f;

            if(AttackTimer <= 0f || AttackStack >= 3)
            {               
                foreach (Collider2D in_collider in in_box_colliders)
                {
                    if (in_collider.tag == "Enemy")
                    {
                        (m_attack_state as PlayerAttackState).EnemyCollider = in_collider;
                        m_player_state_context.Transition(m_attack_state);
                    }
                    else
                    {
                        (m_attack_state as PlayerAttackState).EnemyCollider = null;
                        m_player_state_context.Transition(m_attack_state);
                    }
                }
                AttackStack = 0;
            }*/
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube((Vector2)this.transform.position + new Vector2(m_hit_box_center.x * this.JoyStickDir, m_hit_box_center.y), m_hit_box_size);
        }
    }
}

