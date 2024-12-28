using Junyoung;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


namespace Jongmin
{
    public class ButtonCtrl : MonoBehaviour
    { 
        [Header("Hide Skill Images")]
        [SerializeField] 
        private Image[] m_hide_images;

        [SerializeField]
        private Button[] m_skill_button;

        private PlayerCtrl m_player_ctrl;

        [SerializeField]
        private InventoryManager m_inventory_manager;

        [Header("Buttons")]
        [SerializeField]
        private Button m_attack_button;
        [SerializeField]
        private Button m_seed_button;
        [SerializeField]
        private Button m_jump_button;
        [SerializeField]
        private GameObject m_seed_button_effect;
        [SerializeField]
        private float m_effect_speed;
        private SpriteRenderer m_effect_renderer;
        private bool m_is_effect;

        [SerializeField]
        private StageManager m_stage_manager;

        private void Update()
        {
            if(m_attack_button == null || m_seed_button == null)
            {
                return;
            }

            if(!m_player_ctrl)
            {
                return;
            }

            if(m_player_ctrl.GetSeed)
            {
                SoundManager.Instance.PlayEffect("seed_active", true);
                m_attack_button.gameObject.SetActive(false);
                m_seed_button.gameObject.SetActive(true);
                


                m_stage_manager.GetSeedSpot(SaveManager.Instance.Player.m_stage_id).SetActive(true);

                if(m_player_ctrl.DropSeed)
                {
                    m_seed_button.interactable = true;
                    m_seed_button_effect.SetActive(true);
                }
                else
                {
                    m_seed_button.interactable = false;
                    m_seed_button_effect.SetActive(false);
                }
            }
            else
            {
                SoundManager.Instance.StopEffect(true);
                m_attack_button.gameObject.SetActive(true);
                m_seed_button.gameObject.SetActive(false);

                m_stage_manager.GetSeedSpot(SaveManager.Instance.Player.m_stage_id).SetActive(false);
            }

            if(m_seed_button_effect.activeSelf)
            {                
                if(!m_effect_renderer)
                {
                    m_effect_renderer = m_seed_button_effect.GetComponent<SpriteRenderer>();
                }
                StartCoroutine(SeedButtonEffect());
            }
            else
            {
                m_is_effect = false;
            }
        }

        private IEnumerator SeedButtonEffect()
        {
            m_is_effect = true;
            Color color = m_effect_renderer.color;

            while (m_is_effect)
            {             
                for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * m_effect_speed)
                {
                    color.a = alpha;
                    m_effect_renderer.color = color;
                    yield return null;
                }
                for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * m_effect_speed)
                {
                    color.a = alpha;
                    m_effect_renderer.color = color;
                    yield return null;
                }
            }
        }

        public void TitleClick()
        {
            SoundManager.Instance.PlayEffect("ui_start");

            if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            {
                Debug.Log("이미 캐릭터를 생성한 기록이 있습니다.");
                SceneCtrl.ReplaceScene("Game");
            }
            else
            {
                Debug.Log("캐릭터를 생성한 기록이 없습니다.");
                SceneCtrl.ReplaceScene("Intro");
            }
        }

        public void IntroClick()
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            {
                SceneCtrl.ReplaceScene("Game");
            }
            else
            {
                SceneCtrl.ReplaceScene("PlayerSelect");
            }
        }

        public void PlayClick()
        {
            SceneCtrl.ReplaceScene("Game");
        }

        public void JumpClick()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if(GameManager.Instance.GameStatus == "Playing")
            {
                if(m_player_ctrl.JoyValue.m_joy_touch.y < -0.3f)
                {
                    if(m_player_ctrl.IsDown)
                    {
                        return;
                    }

                    m_player_ctrl.PlayerDown();
                    StartCoroutine(JumpCoolTime(m_hide_images[3], 1f));
                }
                else
                {
                    if(m_player_ctrl.IsJump)
                    {
                        return;
                    }

                    m_player_ctrl.PlayerJump();
                    StartCoroutine(JumpCoolTime(m_hide_images[3], 1.25f));
                }
            }            
        }

        private IEnumerator JumpCoolTime(Image image, float cool_time)
        {
            m_jump_button.interactable = false;
            image.gameObject.SetActive(true);

            float now_time = cool_time;
            while (now_time > 0)
            {
                now_time -= Time.deltaTime;
                image.fillAmount = now_time / cool_time;
                yield return null;
            }

            image.fillAmount = 0f;
            image.gameObject.SetActive(false);
            m_jump_button.interactable = true;
        }

        public void ArrowUp()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.PlayerStop();
        }

        public void AttackClick()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if(!m_player_ctrl.IsAttack)
            {
                Debug.Log($"중첩 되는 중 {m_player_ctrl.AttackStack}");

                switch(GameManager.Instance.CharacterType)
                {
                    case Character.SOCIA:
                        (m_player_ctrl as SociaCtrl).PlayerAttack();
                        break;
                    
                    case Character.GOV:
                        (m_player_ctrl as GovCtrl).PlayerAttack();
                        break;

                    case Character.ENVA:
                        (m_player_ctrl as EnvaCtrl).PlayerAttack();
                        break;
                }
            }
        }

        public void Skill1Click()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerUseSkill1();
                StartCoroutine(CoolDownImageCorutine(m_hide_images[0], 0));
            }
        }

        public void Skill2Click()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerUseSkill2();
                StartCoroutine(CoolDownImageCorutine(m_hide_images[1], 1));

            }
        }

        public void Skill3Click()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerUseSkill3();
                StartCoroutine(CoolDownImageCorutine(m_hide_images[2], 2));

            }
        }
        public IEnumerator CoolDownImageCorutine(Image image, int skill_index)
        {
            float cool_time = m_player_ctrl.m_player_skills[skill_index].m_skill_cool_time;
            m_skill_button[skill_index].interactable = false;
            image.gameObject.SetActive(true);

            float now_time = cool_time;
            while (now_time > 0)
            {
                now_time -= Time.deltaTime;
                image.fillAmount = now_time / cool_time;
                yield return null;
            }

            image.fillAmount = 0f;
            image.gameObject.SetActive(false);
            m_skill_button[skill_index].interactable = true;
        }

        public void CoolDownReset()
        {
            Debug.Log($"스킬 3개, 점프 쿨타임 초기화");
            for(int i=0; i<3; i++)
            {
                m_hide_images[i].fillAmount= 0f;
                m_hide_images[i].gameObject.SetActive(false);
                m_skill_button[i].interactable = true;
            }
            m_hide_images[3].fillAmount = 0f;
            m_hide_images[3].gameObject.SetActive(false);
            m_jump_button.interactable = true;
        }

        public void PlayButtonSound()
        {
            Debug.Log($"Button clicked: {this.gameObject.name}");
            SoundManager.Instance.PlayEffect("ui_click_basic");
        }

        public void PlayButtonSound(string name)
        {
            Debug.Log($"Button clicked: {name}");
            SoundManager.Instance.PlayEffect("ui_click_basic");
        }

        public void InGameSettingAbled()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            m_player_ctrl.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 0f;

            EnemyCtrl[] m_enemies = FindObjectsByType<EnemyCtrl>(FindObjectsSortMode.None);
            foreach(var enemy in m_enemies)
            {
                enemy.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                enemy.GetComponent<Animator>().speed = 0f;
                enemy.IsSetting = true;
            }
        }

        public void InGameSettingDisabled()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            m_player_ctrl.GetComponent<Animator>().speed = 1f;

            EnemyCtrl[] m_enemies = FindObjectsByType<EnemyCtrl>(FindObjectsSortMode.None);
            foreach(var enemy in m_enemies)
            {
                enemy.GetComponent<Animator>().speed = 1f;
                enemy.IsSetting = false;
            }
        }

        public void MainActiveButton()
        {
            GameEventBus.Publish(GameEventType.SETTING);
            GameManager.Instance.ReturnEnemy();
        }
    }
}
