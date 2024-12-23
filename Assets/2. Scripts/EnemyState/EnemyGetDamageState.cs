using Jongmin;
using Junyoung;
using UnityEngine;

public class EnemyGetDamageState : MonoBehaviour, IEnemyState
{
    private EnemyCtrl m_enemy_ctrl;
    private GameObject m_enemy_object;
    private GameObject m_player;

    public Vector2 PlayerVector { get; set; }

    public void Handle(EnemyCtrl enemy)
    {
        if(!m_enemy_ctrl)
        {
            m_enemy_ctrl = enemy;
            m_enemy_object = enemy.gameObject;
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
        PlayerVector = m_player.transform.position;
        m_enemy_ctrl.EnemyStatus.EnemyHP -= SaveManager.Instance.Player.m_player_status.m_strength;
        Debug.Log($"{m_enemy_object.name}가 {SaveManager.Instance.Player.m_player_status.m_strength}데미지를 받음. 현재 체력 : {m_enemy_ctrl.EnemyStatus.EnemyHP}");

        m_enemy_ctrl.StaminaBackground.SetActive(true);
        m_enemy_ctrl.StaminaBar.fillAmount = m_enemy_ctrl.EnemyStatus.EnemyHP / m_enemy_ctrl.OriginalStatus.EnemyHP;

        if (m_enemy_ctrl.EnemyStatus.EnemyHP <= 0f)
        {
            m_enemy_ctrl.EnemyDead();
        }
        else
        {
            StartCoroutine(m_enemy_ctrl.EnemyGetKnockBack(PlayerVector));
        }

        
    }
}
