using Jongmin;
using Junyoung;
using UnityEngine;

public class EnemyGetDamageState : MonoBehaviour, IEnemyState
{
    private EnemyCtrl m_enemy_ctrl;
    private GameObject m_enemy_object;

    public float Damage { get; set; }
    public Vector2 PlayerVector { get; set; }

    public void Handle(EnemyCtrl enemy)
    {
        if(!m_enemy_ctrl)
        {
            m_enemy_ctrl = enemy;
            m_enemy_object = enemy.gameObject;
        }

        m_enemy_ctrl.EnemyStatus.EnemyHP -= SaveManager.Instance.Player.m_player_status.m_strength;

        if(m_enemy_ctrl.EnemyStatus.EnemyHP <= 0f)
        {
            m_enemy_ctrl.EnemyDead();
        }

        StartCoroutine(m_enemy_ctrl.EnemyGetKnockBack(PlayerVector));
    }
}
