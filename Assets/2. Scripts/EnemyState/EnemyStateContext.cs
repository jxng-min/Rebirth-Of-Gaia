using Junyoung;
using UnityEngine;

public class EnemyStateContext
{
    private readonly EnemyCtrl m_enemy_ctrl;

    public IEnemyState m_current_state { get; set; }

    public EnemyStateContext(EnemyCtrl enemy_ctrl)
    {
        m_enemy_ctrl= enemy_ctrl;
    }

    public void Transition()
    {
        m_current_state.Handle(m_enemy_ctrl);
    }

    public void Transition(IEnemyState enemyState)
    {
        m_current_state= enemyState;
        m_current_state.Handle(m_enemy_ctrl);
    }

}
