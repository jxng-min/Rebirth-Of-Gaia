using Junyoung;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private EnemyCtrl m_enemy;

    public void Handle(EnemyCtrl enemy)
    {
        if (!enemy)
        {
            m_enemy = enemy;
        }
            
        Debug.Log($"Enemy MoveState");
    }
}
