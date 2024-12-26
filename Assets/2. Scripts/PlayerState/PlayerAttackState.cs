using UnityEngine;
using Junyoung;
using Jongmin;

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

        if(!m_player_ctrl.IsAttack)
        {           
            if (EnemyCollider)
            {
                EnemyCollider.GetComponent<EnemyCtrl>().EnemyGetDamage();
                SoundManager.Instance.PlayEffect("socia_attack_01");
            }
            else
            {
                SoundManager.Instance.PlayEffect("socia_attack_whoosh");
                Debug.Log($"빚맟췄을때 소리");
            }
            m_player_animator.SetTrigger("Attack1");
            m_player_ctrl.IsAttack = true;
            Invoke("ResetAttackFlag", 0.7f);
            /*
            switch (m_player_ctrl.AttackStack)
            {
                case 1:
                    if(EnemyCollider)
                    {
                        SoundManager.Instance.PlayEffect("socia_attack_01");
                    }
                    else
                    {
                        SoundManager.Instance.PlayEffect("socia_attack_whoosh");
                        Debug.Log($"빚맟췄을때 소리");
                    }
                    Debug.Log("1번 중첩 공격");
                    m_player_animator.SetTrigger("Attack1");
                    break;

                case 2:
                    if (EnemyCollider)
                    {
                        SoundManager.Instance.PlayEffect("socia_attack_02");
                    }
                    else
                    {
                        SoundManager.Instance.PlayEffect("socia_attack_whoosh");
                        Debug.Log($"빚맟췄을때 소리");
                    }
                    Debug.Log("2번 중첩 공격");
                    //m_player_animator.SetTrigger("Attack2");
                    break;

                case 3:
                    if (EnemyCollider)
                    {
                        SoundManager.Instance.PlayEffect("socia_attack_03");
                    }
                    else
                    {
                        SoundManager.Instance.PlayEffect("socia_attack_whoosh");
                        Debug.Log($"빚맟췄을때 소리");
                    }
                    Debug.Log("3번 중첩 공격");
                    //m_player_animator.SetTrigger("Attack3");
                    break;
            }*/
        }          
    }
    

    private void ResetAttackFlag()
    {
        m_player_ctrl.IsAttack = false;
    }
}
