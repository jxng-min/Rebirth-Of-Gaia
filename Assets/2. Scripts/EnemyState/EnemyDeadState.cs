using Jongmin;
using Junyoung;
using System.Collections;
using UnityEngine;

public class EnemyDeadState : MonoBehaviour , IEnemyState
{
    private EnemyCtrl m_enemy_ctrl;
    private StageManager m_stage_manager;
    [SerializeField]
    private SeedShortcutCtrl m_seed_short_cut_ctrl;

    public void Handle(EnemyCtrl enemy_ctrl)
    {
        if(!m_enemy_ctrl)
        {
            m_enemy_ctrl = enemy_ctrl;
            m_stage_manager = GameObject.FindAnyObjectByType<StageManager>();
            m_seed_short_cut_ctrl = GameObject.FindAnyObjectByType<SeedShortcutCtrl>();
        }

        Debug.Log($"Enemy DeadState");
        
        SaveManager.Instance.Player.m_player_status.m_current_exp += m_enemy_ctrl.EnemyStatus.EnemyEx;
        Debug.Log($"플레이어 경험치 {m_enemy_ctrl.EnemyStatus.EnemyEx}만큼 상승. 현재 경험치 : {SaveManager.Instance.Player.m_player_status.m_current_exp}");

        m_stage_manager.m_killed_enemy_num++;
        if(m_stage_manager.m_killed_enemy_num == m_stage_manager.m_total_enemy_num)
        {
            Debug.Log($"마지막 적 처치");
            m_seed_short_cut_ctrl.SpawnSeed(m_enemy_ctrl.transform.position);
            GameEventBus.Publish(GameEventType.TALKING);
        }


        Invoke("DestroyEnemy", 0.25f);
    }


    private void DestroyEnemy()
    {
        m_enemy_ctrl.ReturnToPool();
    }

}
