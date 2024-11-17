using UnityEngine;

namespace Junyoung
{
    public class PlayerCtrl : MonoBehaviour
    {

        private IPlayerState m_stop_state, m_move_state, m_jump_state, m_dead_state, m_clear_state; // PlayerStateContext에 전달하기 위한 각각의 상태들 선언
        private PlayerStateContext m_player_state_context; //플레이어의 상태를 변경해줄 인터페이스 선언

        private void Start()
        {
            m_player_state_context = new PlayerStateContext(this); //Context에 PlayerCtrl 객체 자신을 전달

            m_stop_state = gameObject.AddComponent<PlayerStopState>(); // context를 통해 변경할 상태 스크립트들을 불러옴
            m_move_state = gameObject.AddComponent<PlayerMoveState>();
            m_jump_state = gameObject.AddComponent<PlayerJumpState>();
            m_dead_state = gameObject.AddComponent<PlayerDeadState>();
            m_clear_state = gameObject.AddComponent<PlayerClearState>();

            m_player_state_context.Transition(m_stop_state); // 플레이어의 초기 상태를 정지로 설정
        }

        public void StopPlayer()
        {
            m_player_state_context.Transition(m_stop_state);
        }

        public void MovePlayer()
        {
            m_player_state_context.Transition(m_move_state);
        }

        public void JumpPlayer()
        {
            m_player_state_context.Transition(m_jump_state);
        }

        public void DeadPlayer()
        {
            m_player_state_context.Transition(m_dead_state);
        }

        public void ClearPlayer()
        {
            m_player_state_context.Transition(m_clear_state);
        }
    }
}


