using Jongmin;
using Junyoung;
using UnityEngine;

namespace Junyoung
{
    public class EnemyAttackState : MonoBehaviour, IEnemyState
    {
        private EnemyCtrl m_enemy_ctrl;
        private PlayerCtrl m_player_ctrl;

        public void Handle(EnemyCtrl enemy)
        {
            if (!m_enemy_ctrl)
            {
                m_enemy_ctrl = enemy;
            }

            if(!m_player_ctrl)
            {
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
            }
            
            SoundManager.Instance.PlayEffect("workers_attack_01");
            m_enemy_ctrl.GetComponent<Animator>().SetTrigger("Attack");
            m_player_ctrl.GetComponent<PlayerCtrl>().PlayerGetDamage(m_enemy_ctrl.EnemyStatus.EnemyDamage, m_enemy_ctrl.transform.position);
        }
    }

}

