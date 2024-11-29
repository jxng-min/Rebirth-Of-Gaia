using Junyoung;
using UnityEngine;

public class EnemyDeadState : MonoBehaviour , IEnemyState
{
    private EnemyCtrl m_enemy;

    public void Handle(EnemyCtrl enemy)
    {
        m_enemy = enemy;

        Debug.Log($"Enemy DeadState");
    }
}
