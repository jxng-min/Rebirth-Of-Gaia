using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Junyoung
{
    public class EnemyCtrl : MonoBehaviour
    {
        [SerializeField]
        private EnemyStatus m_original_enemy_status;
        public EnemyStatus OriginalStatus { get { return m_original_enemy_status; } set { m_original_enemy_status = value; } }

        [SerializeField]
        private EnemyStatus m_enemy_status;
        public EnemyStatus EnemyStatus { get { return m_enemy_status; }}

        [SerializeField]
        public float m_speed;

        private IEnemyState m_move_state, m_get_damage_state, m_stop_state, m_dead_state, m_attack_state;
        private EnemyStateContext m_enemy_state_context;

        public IObjectPool<EnemyCtrl> m_managed_pool { get; set; }

        private Rigidbody2D m_rigidbody;

        private int m_dir = -1;

        [SerializeField]
        private Vector2 m_hit_box_size;

        private float m_state_time;
        private float m_loop_time;
        private float m_moving_time;
 
        private SpriteRenderer m_sprite_renderer;

        private bool m_can_attack = true;

        public bool IsKnockBack { get; set; }
        public float KnockBackForce { get ; private set; } = 5f;

        private void OnEnable()
        {
            if (!m_enemy_status)
            {
                m_enemy_status = ScriptableObject.CreateInstance<EnemyStatus>();
            }
            if (m_original_enemy_status)
            {
                InitStatus();
            }
        }

        public void InitStatus()
        {
            m_enemy_status.EnemyType = m_original_enemy_status.EnemyType;
            m_enemy_status.EnemyHP = m_original_enemy_status.EnemyHP;
            m_enemy_status.EnemyDamage = m_original_enemy_status.EnemyDamage;
            m_enemy_status.EnemyAttackDelay = m_original_enemy_status.EnemyAttackDelay;
            m_enemy_status.EnemyMoveSpeed = m_original_enemy_status.EnemyMoveSpeed;
            m_enemy_status.EnemyEx = m_original_enemy_status.EnemyEx;
        }


        public void SetEnemyPool(IObjectPool<EnemyCtrl> pool)
        {
            m_managed_pool = pool;
        }

        public void ReturnToPool()
        {
            Debug.Log($"Enemy 반환");
            m_managed_pool.Release(this);
        }

        private void Start()
        {
            m_speed = m_enemy_status.EnemyMoveSpeed;

            m_move_state = gameObject.AddComponent<EnemyMoveState>();
            m_get_damage_state = gameObject.AddComponent<EnemyGetDamageState>();
            m_stop_state = gameObject.AddComponent<EnemyStopState>();
            m_dead_state = gameObject.AddComponent<EnemyDeadState>();
            m_attack_state = gameObject.AddComponent<EnemyAttackState>();

            m_enemy_state_context = new EnemyStateContext(this);
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_sprite_renderer = GetComponent<SpriteRenderer>();

            SetPatrolTime();
        }

        private void Update()
        {
            Collider2D[] in_box_colliders = Physics2D.OverlapBoxAll(this.transform.position, m_hit_box_size, 0);
            foreach (Collider2D in_collider in in_box_colliders)
            {
                if (in_collider.gameObject.layer == LayerMask.NameToLayer("PLAYER"))
                {
                    if(m_can_attack)
                    {
                        m_can_attack = false;
                        StartCoroutine(DelayAttack());
                    }    
                }
            }
        }

        private void FixedUpdate()
        {
            if(!IsKnockBack)
            {
                if (m_state_time > m_moving_time)
                {
                    m_state_time -= Time.deltaTime;
                    EnemyMove();
                }
                else if (m_state_time > 0)
                {
                    EnemyStop();
                    m_state_time -= Time.deltaTime;
                }
                else
                {
                    ChangeDirection();
                    m_state_time = m_loop_time;
                }
            }
        }

        public void EnemyAttack()
        {
            m_enemy_state_context.Transition(m_attack_state);
        }

        public void EnemyMove()
        {
            GroundChecker();

            m_rigidbody.linearVelocityX = m_dir * m_enemy_status.EnemyMoveSpeed;

            m_enemy_state_context.Transition(m_move_state);
        }

        public void EnemyStop()
        {
            m_enemy_state_context.Transition(m_stop_state);
        }

        public void EnemyGetDamage()
        {
            m_enemy_state_context.Transition(m_get_damage_state);
        }

        public void EnemyDead()
        {
            m_enemy_state_context.Transition(m_dead_state);
        }

        public void SetPatrolTime()
        {
            m_loop_time = Random.Range(6f, 11f);
            m_moving_time = Random.Range(0f, 4f);

            m_state_time= m_loop_time;
        }

        private void ChangeDirection()
        {
            m_dir *= -1;
            m_sprite_renderer.flipX = !(m_sprite_renderer.flipX);
        }

        // 낭떠러지 유무를 확인하는 메소드
        private void GroundChecker()
        {
            Vector2 frontVec = new Vector2(m_rigidbody.position.x + m_dir * 1.2f ,m_rigidbody.position.y);

            Debug.DrawRay(frontVec, Vector3.down, new Color(0f, 1f ,0f));
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1.5f, LayerMask.GetMask("GROUND"));

            if(raycast.collider == null)
            {
                ChangeDirection();
            }
        }

        // 공격이 불가능한 상태로 변경하는 코루틴
        private IEnumerator DelayAttack()
        {
            EnemyAttack();

            yield return new WaitForSeconds(m_enemy_status.EnemyAttackDelay);

            m_can_attack= true;
        }

        public void TestEnemyDead()
        {
            Invoke("EnemyDead", 5f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(this.transform.position, m_hit_box_size);
        }

        public IEnumerator EnemyGetKnockBack(Vector2 player_vector)
        {
            IsKnockBack = true;

            Vector2 dir = ((Vector2)transform.position - player_vector).normalized;
            m_rigidbody.AddForce(dir * KnockBackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(1f);

            IsKnockBack = false;
        }
    }
}