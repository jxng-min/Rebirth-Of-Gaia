using Junyoung;
using UnityEngine;
using UnityEngine.Pool;


namespace Junyoung
{
    public class EnemyCtrl : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        [SerializeField]
        public EnemyStatus m_enemy_status;

        private IEnemyState m_move_state, m_get_damage_state, m_stop_state, m_dead_state;
        private EnemyStateContext m_enemy_state_context;

        public IObjectPool<EnemyCtrl> m_managed_pool { get; set; }

        private Rigidbody2D m_rigidbody;

        private int m_dir=-1;

        public void testEnemyDead()
        {
            Invoke("EnemyDead", 5f);
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

        void Start()
        {
            m_move_state = gameObject.AddComponent<EnemyMoveState>();
            m_get_damage_state = gameObject.AddComponent<EnemyGetDamageState>();
            m_stop_state = gameObject.AddComponent<EnemyStopState>();
            m_dead_state = gameObject.AddComponent<EnemyDeadState>();

            m_enemy_state_context = new EnemyStateContext(this);

            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            GroundChecker();
            m_rigidbody.linearVelocityX = m_dir * m_enemy_status.EnemyMoveSpeed;
        }

        private void GroundChecker()// 앞이 낭떨어지인지 체크하는 함수
        {
            Vector2 frontVec = new Vector2(m_rigidbody.position.x + m_dir * 1.2f ,m_rigidbody.position.y);
            Debug.DrawRay(frontVec,Vector3.down, new Color(0,1,0));
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1.5f, LayerMask.GetMask("GROUND"));
            if(raycast.collider == null)
            {
                m_dir *= -1;
            }
        
        }

        public void EnemyMove()
        {
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

    }
}

