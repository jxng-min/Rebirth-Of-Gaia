using Jongmin;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Junyoung
{
    public abstract class PlayerCtrl : MonoBehaviour
    {
        [Header("Joystick")]
        [SerializeField]
        private JoyStickValue m_value;

        [Header("Physics")]
        private Rigidbody2D m_rigidbody;

        [SerializeField]
        private LayerMask m_item_layer;

        [SerializeField]
        private ItemData m_item;

        [Header("Manager")]
        [SerializeField]
        private InventoryManager m_inventory_manager;

        public float MoveSpeed { get; private set; }
        private Vector2 m_move_vec = Vector2.zero;

        public Vector2 MoveVector
        {
            get { return m_move_vec; }
            set { m_move_vec = value; }
        }
     
        [Header("State")]
        private IPlayerState m_stop_state, m_move_state, m_jump_state, m_dead_state, m_clear_state, m_down_state, m_fall_state, m_get_damage_state;
        protected IPlayerState m_attack_state;
        protected PlayerStateContext m_player_state_context;

        public float JumpPower { get; private set; }
        public bool IsGrounded { get; set; }
        public bool IsJump { get; set; }
        public bool IsDown { get; set; }
        public bool IsFall { get; set; }
        private float m_last_height;

        [Header("About Attack")]
        public bool IsAttack { get; set;}
        public int AttackStack { get; set; }
        public float InputDelay { get; set; } = 0.3f;
        public float AttackTimer { get; set; } = 0f;

        [Header("About KnockBack")]
        public bool IsKnockBack { get; set; }
        public float KnockBackForce { get ; private set; } = 10f;
        
        [Header("Skill")]
        public Skill[] m_player_skills = new Skill[3];

        private void OnEnable()
        {
            GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
            GameEventBus.Subscribe(GameEventType.SETTING, GameManager.Instance.Setting);
            GameEventBus.Subscribe(GameEventType.DEAD, GameManager.Instance.Dead);
            GameEventBus.Subscribe(GameEventType.CLEAR, GameManager.Instance.Clear);
            GameEventBus.Subscribe(GameEventType.FINISH, GameManager.Instance.Finish);
            GameEventBus.Subscribe(GameEventType.CONQUER, GameManager.Instance.Conquer);

        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
            GameEventBus.Unsubscribe(GameEventType.SETTING, GameManager.Instance.Setting);
            GameEventBus.Unsubscribe(GameEventType.DEAD, GameManager.Instance.Dead);
            GameEventBus.Unsubscribe(GameEventType.CLEAR, GameManager.Instance.Clear);
            GameEventBus.Unsubscribe(GameEventType.FINISH, GameManager.Instance.Finish);
            GameEventBus.Unsubscribe(GameEventType.CONQUER, GameManager.Instance.Conquer);
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
            m_fall_state = gameObject.AddComponent<PlayerFallState>();
            m_get_damage_state = gameObject.AddComponent<PlayerGetDamageState>();
            m_attack_state = gameObject.AddComponent<PlayerAttackState>();
            
            m_player_state_context.Transition(m_stop_state);
            
            m_inventory_manager = gameObject.AddComponent<InventoryManager>();

            m_rigidbody = GetComponent<Rigidbody2D>();
            gameObject.AddComponent<ObjectScanCtrl>();
            gameObject.AddComponent<PlatformScanCtrl>();

            SetPlayerSkill();

            MoveSpeed = 4.0f;
            JumpPower = 15.0f;
        }

        private void Update()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                if((!IsDown && !IsJump))
                {
                    float current_height = m_rigidbody.linearVelocity.y;
                    if(!IsFall)
                    {
                        if(m_last_height - current_height > 0.2f)
                        {
                            Debug.Log("떨어지는 상태 호출됨");
                            PlayerFall();
                        }

                        m_last_height = current_height;
                    }
                    m_last_height = current_height = m_rigidbody.linearVelocity.y;
                }

                PlayerAttack();
            }
        }

        private void FixedUpdate()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                float joystick_value = 0f;
                if(!IsAttack)
                {
                    if(m_value.m_joy_touch.x < 0f)
                    {
                        joystick_value = -1f;
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if(m_value.m_joy_touch.x > 0f)
                    {
                        joystick_value = 1f;
                        GetComponent<SpriteRenderer>().flipX = false;
                    }

                    SetPlayerMoveState();
                }

                if(!IsKnockBack)
                {
                    m_rigidbody.linearVelocity = new Vector2(joystick_value * MoveSpeed, m_rigidbody.linearVelocity.y);
                }
            }
        }
        
        protected void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("ITEM") )
            {
                if(col.gameObject.tag == "Seed")
                {
                    Debug.Log($"씨앗 감지");
                    if(m_inventory_manager!= null)
                    {
                        Debug.Log("인벤토리 매니저 널");

                    }
                    else
                    {
                        m_inventory_manager.AcquireItem(col.gameObject.GetComponent<SeedCtrl>().m_seed_data);
                        Destroy(col.gameObject);
                    }

                }
                
            }

        }

        public void PlayerGetDamage(float damage, Vector2 enemy_vector)
        {          
            m_player_state_context.Transition(m_get_damage_state);
        }

        public abstract void PlayerAttack();

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
            if(!IsDown && GameManager.Instance.GameStatus == "Playing")
            {
                m_player_state_context.Transition(m_down_state);
            }
        }

        public void PlayerJump()
        {
            if(!IsJump && IsGrounded && GameManager.Instance.GameStatus == "Playing")
            {
                m_player_state_context.Transition(m_jump_state);       
            }
        }

        public void PlayerFall()
        {
            m_player_state_context.Transition(m_fall_state);
        }

        public void PlayerDead()
        {
            m_player_state_context.Transition(m_dead_state);
        }

        public void PlayerClear()
        {
            m_player_state_context.Transition(m_clear_state);
        }

        private void SetPlayerMoveState()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                if(m_value.m_joy_touch == Vector2.zero)
                {
                    PlayerStop();
                }
                else
                {
                    PlayerMove();
                }
            }
        }

        public IEnumerator PlayerGetKnockBack(Vector2 enemy_vector)
        {
            IsKnockBack = true;

            Vector2 dir = ((Vector2)transform.position - enemy_vector).normalized;
            m_rigidbody.AddForce(dir * KnockBackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.5f);

            IsKnockBack = false;
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