using UnityEngine;
using Junyoung;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private PlayerCtrl m_player_ctrl;
    private Animator m_player_animator;
    public Collider2D EnemyCollider { get; set; }

    public void Handle(PlayerCtrl player_ctrl)
    {
        if(!m_player_ctrl)
        {
            m_player_ctrl = player_ctrl;
            m_player_animator = player_ctrl.GetComponent<Animator>();
        }

        Debug.Log("공격 상태로 전환됩니다.");

        m_player_ctrl.IsAttack = true;

        switch(m_player_ctrl.AttackStack)
        {
            case 1:
                Debug.Log("1번 중첩 공격");
                m_player_animator.SetTrigger("Attack1");
                break;

            case 2:
                Debug.Log("2번 중첩 공격");
                //m_player_animator.SetTrigger("Attack2");
                break;

            case 3:
                Debug.Log("3번 중첩 공격");
                //m_player_animator.SetTrigger("Attack3");
                break;
        }
        if(EnemyCollider)
        {
            EnemyCollider.GetComponent<EnemyCtrl>().EnemyGetDamage();
        }

        Invoke("ResetAttackFlag", 1f);
    }

    private void ResetAttackFlag()
    {
        m_player_ctrl.IsAttack = false;
    }
}
