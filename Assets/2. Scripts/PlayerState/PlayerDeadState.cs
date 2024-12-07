using UnityEngine;

namespace Junyoung
{
    public class PlayerDeadState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;
            }

            m_player_ctrl.GetComponent<Animator>().speed = 0f;
            m_player_ctrl.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}