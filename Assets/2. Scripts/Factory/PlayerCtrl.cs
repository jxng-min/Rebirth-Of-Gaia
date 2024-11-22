using Jongmin;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Junyoung
{
    public abstract class PlayerCtrl : MonoBehaviour
    {
        public Rigidbody2D m_rigidbody;

        public float MoveSpeed { get; private set; }
        public float JumpPower { get; private set; }

        public Vector2 m_move_vec = Vector2.zero;
        public bool m_is_jump = false;
     

        private IPlayerState m_stop_state, m_move_state, m_jump_state, m_dead_state, m_clear_state, m_down_state; // 각 상태들의 선언
        private PlayerStateContext m_player_state_context;                                          //상태를 변경할 인터페이스 선언

        public Skill[] m_player_skills = new Skill[3]; 

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
            m_down_state = gameObject.AddComponent<PlayerDownState>();

            m_player_state_context.Transition(m_stop_state);                // 플레이어의 초기 상태를 정지 상태로 설정

            SetPlayerSkill();

            MoveSpeed = 4.0f;
            JumpPower = 15.0f;

            gameObject.AddComponent<ObjectScanCtrl>();
            gameObject.AddComponent<PlatformScanCtrl>();
        }

        private void Update()
        {
            if(m_move_vec.x != 0f)
                PlayerMove();
            else
                PlayerStop();
        }

        private void FixedUpdate()
        {
            m_rigidbody.linearVelocity = new Vector2(m_move_vec.x * MoveSpeed, m_rigidbody.linearVelocity.y);
        }

        public void PlayerStop()
        {
            m_player_state_context.Transition(m_stop_state);
        }

        public void PlayerMove()
        {
            m_player_state_context.Transition(m_move_state);
        }

        public void PlayerDown()
        {
            if(GameManager.Instance.m_game_status == "Playing")
            {
                m_player_state_context.Transition(m_down_state);
            }
        }

        public void PlayerJump()
        {
            if(!m_is_jump && GameManager.Instance.m_game_status == "Playing")
            {
                m_player_state_context.Transition(m_jump_state);       
            }
        }

        public void DeadPlayer()
        {
            m_player_state_context.Transition(m_dead_state);
        }

        public void ClearPlayer()
        {
            m_player_state_context.Transition(m_clear_state);
        }

        public abstract void SetPlayerSkill();
    }
}