using Jongmin;
using Junyoung;
using UnityEngine;

public class EnemyGetDamageState : MonoBehaviour, IEnemyState
{
    private EnemyCtrl m_enemy_ctrl;
    private GameObject m_enemy_object;

    private SaveManager m_save_manager;

    public float Damage { get; set; }
    public Vector2 PlayerVector { get; set; }

    public void Handle(EnemyCtrl enemy)
    {
        if(!m_enemy_ctrl)
        {
            m_enemy_ctrl = enemy;
            m_enemy_object = enemy.gameObject;
        }

        if(!m_save_manager)
        {
            m_save_manager = FindAnyObjectByType<SaveManager>();
        }

        m_enemy_ctrl.EnemyStatus.EnemyHP -= m_save_manager.Player.m_player_status.m_strength;

        if(m_enemy_ctrl.EnemyStatus.EnemyHP <= 0f)
        {
            m_enemy_ctrl.EnemyDead();
        }

        StartCoroutine(m_enemy_ctrl.EnemyGetKnockBack(PlayerVector));
    }
}
