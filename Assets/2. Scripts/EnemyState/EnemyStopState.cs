using Junyoung;
using UnityEngine;

public class EnemyStopState : MonoBehaviour, IEnemyState
{
    private EnemyCtrl m_enemy_ctrl;

    public void Handle(EnemyCtrl enemy_ctrl)
    {
        if(!m_enemy_ctrl)
        {
            m_enemy_ctrl = enemy_ctrl;
        }
    }
}
