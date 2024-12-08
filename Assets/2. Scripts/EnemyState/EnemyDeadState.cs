using Junyoung;
using System.Collections;
using UnityEngine;

public class EnemyDeadState : MonoBehaviour , IEnemyState
{
    private EnemyCtrl m_enemy;

    public void Handle(EnemyCtrl enemy)
    {
        if(!m_enemy)
        {
            m_enemy = enemy;
        }
            

        Debug.Log($"Enemy DeadState");


        Invoke("DestroyEnemy", 0.25f);
    }


    private void DestroyEnemy()
    {
        m_enemy.ReturnToPool();
    }

}
