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

        public JoyStickValue JoyValue { get { return m_value; } }

        public float JoyStickDir { get; set; } = 1f;

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
        public bool IsMove { get; set;}

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
        public bool IsAttack { get; set;} = false;
        public int AttackStack { get; set; }
        public float InputDelay { get; set; } = 0.3f;
        public float AttackTimer { get; set; } = 0f;

        [Header("About KnockBack")]
        public bool IsKnockBack { get; set; }
        public float KnockBackForce { get ; private set; } = 6.5f;
        
        [Header("Skill")]
        public Skill[] m_player_skills = new Skill[3];

        public bool GetSeed { get; set; }
        public bool DropSeed { get; set;}

       

        private void OnEnable()
        {
            GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
            GameEventBus.Subscribe(GameEventType.SETTING, GameManager.Instance.Setting);
            GameEventBus.Subscribe(GameEventType.DEAD, GameManager.Instance.Dead);
            GameEventBus.Subscribe(GameEventType.CLEAR, GameManager.Instance.Clear);
            GameEventBus.Subscribe(GameEventType.FINISH, GameManager.Instance.Finish);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
            GameEventBus.Unsubscribe(GameEventType.SETTING, GameManager.Instance.Setting);
            GameEventBus.Unsubscribe(GameEventType.DEAD, GameManager.Instance.Dead);
            GameEventBus.Unsubscribe(GameEventType.CLEAR, GameManager.Instance.Clear);
            GameEventBus.Unsubscribe(GameEventType.FINISH, GameManager.Instance.Finish);
        }

        private void Start()
        {
            GameEventBus.Publish(GameEventType.SETTING);

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
            
            m_inventory_manager = GameObject.FindAnyObjectByType<InventoryManager>();

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
                if((!IsDown && !IsJump && !IsKnockBack))
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
            }
        }

        private void FixedUpdate()
        {
            if(GameManager.Instance.GameStatus == "Playing" )
            {
                if(!IsAttack && !IsKnockBack)
                {
                    if(m_value.m_joy_touch.x < 0f)
                    {
                        JoyStickDir = -1f;
                        GetComponent<SpriteRenderer>().flipX = true;
                        m_rigidbody.linearVelocityX = JoyStickDir * MoveSpeed;
                    }
                    else if(m_value.m_joy_touch.x > 0f)
                    {
                        JoyStickDir = 1f;
                        GetComponent<SpriteRenderer>().flipX = false;
                        m_rigidbody.linearVelocityX = JoyStickDir * MoveSpeed;
                    }

                    SetPlayerMoveState();
                    
                }                   
            }
        }
        
        protected void OnCollisionEnter2D(Collision2D col) //아이템 습득
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("ITEM") )
            {
                if(col.gameObject.tag == "Seed")
                {
                    if(m_inventory_manager== null)
                    {
                        Debug.Log("인벤토리 매니저 널");
                    }
                    else
                    {
                        //StartCoroutine(SoundManager.Instance.FadeBackground("bgm_battle_amb"));
                        SoundManager.Instance.PlayEffect("seed_get");
                        GetSeed = true;
                        Destroy(col.gameObject);
                    }
                }
            }
        }

        protected void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("닿고 있다.");
            if(GetSeed)
            {
                if(col.gameObject.tag == "Spot")
                {
                    GameEventBus.Publish(GameEventType.TALKING);
                    SoundManager.Instance.PlayEffect("seed_active");
                    DropSeed = true;
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D col)
        {
            if (GetSeed)
            {
                if (col.CompareTag("Spot"))
                {
                    DropSeed = false;
                }
            }       
        }

        public void PlayerGetDamage(float damage, Vector2 enemy_vector)
        {
            (m_get_damage_state as PlayerGetDamageState).Damage = damage;
            (m_get_damage_state as PlayerGetDamageState).EnemyVector = enemy_vector;
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

        public abstract void PlayerUseSkill1();
        public abstract void PlayerUseSkill2();
        public abstract void PlayerUseSkill3();


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
            dir.y += 1f;
            dir = dir.normalized;
            //m_rigidbody.AddForce(dir * KnockBackForce, ForceMode2D.Impulse);
            m_rigidbody.linearVelocity = dir * KnockBackForce;

            yield return new WaitForSeconds(1f);

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