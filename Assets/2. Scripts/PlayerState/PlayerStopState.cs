using UnityEngine;

namespace Junyoung
{
    public class PlayerStopState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;
        private Rigidbody2D m_player_rigidbody;
        public void Handle(PlayerCtrl player_ctrl)
        {
            if(!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;
            }

        }


        void FixedUpdate() 
        {
            
            //m_player_rigidbody.linearVelocity = new Vector2(0, m_player_rigidbody.linearVelocity.y);

        }
    }
}


