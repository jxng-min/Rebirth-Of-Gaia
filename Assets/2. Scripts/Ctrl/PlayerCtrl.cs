using Jongmin;
using Unity.VisualScripting;
using UnityEngine;

namespace Junyoung
{
    public class PlayerCtrl : MonoBehaviour
    {
        public Rigidbody2D m_rigidbody;

        private float m_walk_speed = 4.0f;
        private float m_jump_power = 5.0f;
        private int m_dir = 0;
        // PlayerMoveState에서 이동 속도와 방향을 일기만 할 수 있게 프로퍼티 작성
        public float WalkSpeedP { get { return m_walk_speed; } private set { m_walk_speed = value; } }
        public float JumpPowerP { get { return m_jump_power; } private set { m_jump_power = value; } }
        public int DirP { get { return m_dir; } private set { m_dir = value; } }

        public Vector2 m_move_vec = Vector2.zero;
        public bool m_is_jump = false;

        public bool m_is_grunded = false;

        private IPlayerState m_stop_state, m_move_state, m_jump_state, m_dead_state, m_clear_state; // 각 상태들의 선언
        private PlayerStateContext m_player_state_context;                                          //상태를 변경할 인터페이스 선언

        private void OnEnable()
        {
            GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        }


        private void Start()
        {
            GameEventBus.Publish(GameEventType.PLAYING);
            
            m_rigidbody = GetComponent<Rigidbody2D>();

            m_player_state_context = new PlayerStateContext(this);          //Context에 PlayerCtrl 객체 자신을 전달

            m_stop_state = gameObject.AddComponent<PlayerStopState>();      // context를 통해 변경할 상태 스크립트들을 컴포넌트로 추가
            m_move_state = gameObject.AddComponent<PlayerMoveState>();
            m_jump_state = gameObject.AddComponent<PlayerJumpState>();
            m_dead_state = gameObject.AddComponent<PlayerDeadState>();
            m_clear_state = gameObject.AddComponent<PlayerClearState>();

            m_player_state_context.Transition(m_stop_state);                // 플레이어의 초기 상태를 정지 상태로 설정
        }

        public void PlayerMoveLeftBtnDown() // 좌측 이동 버튼 클릭으로 플레이어를 이동시킴
        {
            m_dir = -1;
            m_player_state_context.Transition(m_move_state);
        }

        public void PlayerMoveRightBtnDown()// 우측 이동 버튼 클릭으로 플레이어를 이동시킴
        {
            m_dir = 1;
            m_player_state_context.Transition(m_move_state);
        }

        public void PlayerMoveBtnUP() // 플레이어가 버튼에서 손을 땠을 때 움직임을 정지 시킴
        {
            m_dir = 0;
            m_player_state_context.Transition(m_stop_state);
        }

        public void PlayerJump()
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


