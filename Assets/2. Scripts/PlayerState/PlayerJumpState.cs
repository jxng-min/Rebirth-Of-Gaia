using Jongmin;
using System.Collections;
using UnityEngine;

namespace Junyoung
{
    public class PlayerJumpState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;                            
            }

            SoundManager.Instance.PlayEffect("socia_jump_up");
            m_player_ctrl.IsJump = true;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * m_player_ctrl.JumpPower;
            GetComponent<Animator>().SetTrigger("Jump");
            StartCoroutine(Jumping());
        }

        private IEnumerator Jumping()
        {
            yield return new WaitForSeconds(1.25f);
            m_player_ctrl.IsJump = false;
        } 
    }
}