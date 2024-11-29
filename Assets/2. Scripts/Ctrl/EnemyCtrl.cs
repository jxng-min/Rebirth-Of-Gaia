using Junyoung;
using UnityEngine;


namespace Junyoung
{
    public class EnemyCtrl : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private IEnemyState m_move_state, m_get_damage_state, m_stop_state, m_dead_state;
        private EnemyStateContext m_enemy_state_context;

        void Start()
        {
            m_move_state = gameObject.AddComponent<EnemyMoveState>();
            m_get_damage_state = gameObject.AddComponent<EnemyGetDamageState>();
            m_stop_state = gameObject.AddComponent<EnemyStopState>();
            m_dead_state = gameObject.AddComponent<EnemyDeadState>();

            m_enemy_state_context = new EnemyStateContext(this);
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

