using Junyoung;
using System.Collections;
using UnityEngine;

public class PlayerFallState : MonoBehaviour, IPlayerState
{
    private PlayerCtrl m_player_ctrl;

    public void Handle(PlayerCtrl player_ctrl)
    {
        if(!m_player_ctrl)
        {
            m_player_ctrl = player_ctrl;
        }

        m_player_ctrl.IsFall = true;
        GetComponent<Animator>().SetTrigger("Fall");
        StartCoroutine(Falling());
    }

    private IEnumerator Falling()
    {
        yield return new WaitForSeconds(1.0f);
        m_player_ctrl.IsFall = false;
    }
}
