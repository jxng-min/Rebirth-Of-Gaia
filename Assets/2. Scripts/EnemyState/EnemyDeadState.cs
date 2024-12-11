using Junyoung;
using System.Collections;
using UnityEngine;

public class EnemyDeadState : MonoBehaviour , IEnemyState
{
    private EnemyCtrl m_enemy_ctrl;

    public void Handle(EnemyCtrl enemy_ctrl)
    {
        if(!m_enemy_ctrl)
        {
            m_enemy_ctrl = enemy_ctrl;
        }

        Invoke("DestroyEnemy", 0.25f);
    }

    private void DestroyEnemy()
    {
        m_enemy_ctrl.ReturnToPool();
    }

}
