using UnityEngine;

namespace Junyoung
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;
        private int m_dir;
        private float m_walk_speed;
        private Rigidbody2D m_player_rigidbody;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl) // null일때 최초 한번만 작동해서 초기화 하는 역할
            {
                m_player_ctrl = player_ctrl;
                
                m_walk_speed = m_player_ctrl.WalkSpeedP;
                m_player_rigidbody = m_player_ctrl.m_rigidbody;
            }
        }


        void FixedUpdate() //프레임과 별도로 일정한 텀을 가지고 동작 -> 프레임간 시간 차이를 고려하지 않아도 됨
        {
            m_dir = m_player_ctrl.DirP;
            // velocity는 질량과 관성등을 무시하고 입력된 속도로 움직여서, 미끄러짐 현상이 없음(정밀한 컨트롤 가능)
            m_player_rigidbody.linearVelocity = new Vector2(m_dir * m_walk_speed, m_player_rigidbody.linearVelocity.y);  

        }
    }
}