using Jongmin;
using Junyoung;
using UnityEngine;

namespace Junyoung
{
    public class EnemyAttackState : MonoBehaviour, IEnemyState
    {
        private EnemyCtrl m_enemy;
        private GameObject m_player;

        private void Start()
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Handle(EnemyCtrl enemy)
        {
            if (!m_enemy)
            {
                m_enemy = enemy;
            }
            m_player.GetComponent<PlayerCtrl>().PlayerGetDamage(m_enemy.m_enemy_status.EnemyDamage, m_enemy.transform.position); //스테이터스의 데미지 만큼 플레이어에게 데미지를 줌
            Debug.Log($"Enemy : Attack to Player");
        }
    }

}

