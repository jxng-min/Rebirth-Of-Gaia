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

            m_player_ctrl.m_rigidbody.linearVelocity = Vector2.up * m_player_ctrl.JumpPower;
        }
    }
}