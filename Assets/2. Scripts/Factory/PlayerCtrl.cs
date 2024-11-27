using Jongmin;
using UnityEditor.Rendering;
using UnityEngine;

namespace Junyoung
{
    public abstract class PlayerCtrl : MonoBehaviour
    {
        [Header("Physics")]
        private Rigidbody2D m_rigidbody;

        public float MoveSpeed { get; private set; }
        public float JumpPower { get; private set; }

        private Vector2 m_move_vec = Vector2.zero;

        public Vector2 MoveVector
        {
            get { return m_move_vec; }
            set { m_move_vec = value; }
        }
     
        [Header("State")]

        private IPlayerState m_stop_state, m_move_state, m_jump_state, m_dead_state, m_clear_state, m_down_state;
        private PlayerStateContext m_player_state_context;

        public bool IsJump { get; set; }

        [Header("Skill")]
        public Skill[] m_player_skills = new Skill[3];

        private void OnEnable()
        {
            GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
            GameEventBus.Subscribe(GameEventType.SETTING, GameManager.Instance.Setting);
            GameEventBus.Subscribe(GameEventType.DEAD, GameManager.Instance.Dead);
            GameEventBus.Subscribe(GameEventType.FINISH, GameManager.Instance.Finish);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
            GameEventBus.Unsubscribe(GameEventType.SETTING, GameManager.Instance.Setting);
            GameEventBus.Unsubscribe(GameEventType.DEAD, GameManager.Instance.Dead);
            GameEventBus.Unsubscribe(GameEventType.FINISH, GameManager.Instance.Finish);
        }

        private void Start()
        {
            GameEventBus.Publish(GameEventType.PLAYING);

            m_player_state_context = new PlayerStateContext(this);

            m_stop_state = gameObject.AddComponent<PlayerStopState>();
            m_move_state = gameObject.AddComponent<PlayerMoveState>();
            m_jump_state = gameObject.AddComponent<PlayerJumpState>();
            m_dead_state = gameObject.AddComponent<PlayerDeadState>();
            m_clear_state = gameObject.AddComponent<PlayerClearState>();
            m_down_state = gameObject.AddComponent<PlayerDownState>();

            m_player_state_context.Transition(m_stop_state);

            m_rigidbody = GetComponent<Rigidbody2D>();
            gameObject.AddComponent<ObjectScanCtrl>();
            gameObject.AddComponent<PlatformScanCtrl>();

            SetPlayerSkill();

            MoveSpeed = 4.0f;
            JumpPower = 15.0f;
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
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_state_context.Transition(m_down_state);
            }
        }

        public void PlayerJump()
        {
            if(!IsJump && GameManager.Instance.GameStatus == "Playing")
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

        public void SetPlayerSkill(int index, int skill_id)
        {
            if(index > 2)
            {
                Debug.Log("스킬은 최대 3개를 가질 수 있습니다. 현재 인덱스를 넘어서 참조하고 있습니다.");
                return;
            }

            switch(skill_id)
            {
            case 0:
                m_player_skills[index] = new SociaSkill1();
            break;
            }
        }
    }
}