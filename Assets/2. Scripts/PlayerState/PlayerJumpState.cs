using UnityEngine;

namespace Junyoung
{
    public class PlayerJumpState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;
        private float m_jump_power;
        private Rigidbody2D m_player_rigidbody;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;
                m_jump_power = m_player_ctrl.JumpPowerP;
                m_player_rigidbody = m_player_ctrl.m_rigidbody;                              
            }

            m_player_rigidbody.linearVelocity = Vector2.up * m_jump_power; // if문 안에 작성시 최초 한번만 동작함
        }


    }
}