using Junyoung;
using System;
using System.Collections;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEngine;

namespace Jongmin
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerCtrl m_player_ctrl;
        private ButtonCtrl m_button_ctrl;

        public string GameStatus { get; private set; }
        public Character CharacterType{ get; set; }

        private void Start()
        {
            GameEventBus.Subscribe(GameEventType.NONE, None);
            GameEventBus.Subscribe(GameEventType.LOADING, Loading);
            GameEventBus.Publish(GameEventType.NONE);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void None()
        {
            GameStatus = "None";
            SoundManager.Instance.PlayBGM("bgm_main");
            //SoundManager.Instance.FadeBackground("bgm_main");
        }

        public void Loading()
        {
            GameStatus = "Loading";
        }

        public void Playing()
        {
            GameStatus = "Playing";

            m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();

            m_player_ctrl.JoyValue.m_joy_touch = Vector2.zero;
            m_player_ctrl.GetComponent<SpriteRenderer>().flipX = false;
            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 1f;

            //SoundManager.Instance.PlayBGM("bgm_battle");
            StartCoroutine(SoundManager.Instance.FadeBackground("bgm_battle"));

            AbleUI();

            Recover();

            m_button_ctrl = FindAnyObjectByType<ButtonCtrl>();
            m_button_ctrl.CoolDownReset();
                      
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Setting()
        {
            GameStatus = "Setting";

            //SoundManager.Instance.PlayBGM("bgm_lobby");
            StartCoroutine(SoundManager.Instance.FadeBackground("bgm_lobby"));
            Recover();

            Destroy(GameObject.FindGameObjectWithTag("Seed"));

            if(m_player_ctrl)
            {
                m_player_ctrl.MoveVector = Vector2.zero;
                m_player_ctrl.GetComponent<Animator>().speed = 0f;
                m_player_ctrl.GetSeed = false;
                m_player_ctrl.DropSeed = false;
            }

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Dead()
        {
            GameStatus = "Dead";

            m_player_ctrl.PlayerDead();

            ReturnEnemy();

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 0f;
            m_player_ctrl.GetSeed = false;
            m_player_ctrl.DropSeed = false;

            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlayEffect("stage_fail");

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(true);
        }

        public void Clear()
        {
            GameStatus = "Clear";
          
            SaveManager m_save_manager = FindAnyObjectByType<SaveManager>();
            StageManager m_stage_manager = FindAnyObjectByType<StageManager>();

            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlayEffect("stage_clear");

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 0f;
            m_player_ctrl.GetSeed = false;
            m_player_ctrl.DropSeed = false;

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);

            if (m_save_manager.Player.m_stage_id < m_stage_manager.m_max_stage && m_save_manager.Player.m_stage_id == m_save_manager.Player.m_max_clear_stage)
            {
                Debug.Log($"스테이지를 클리어 해서 최고 스테이지를 {m_save_manager.Player.m_max_clear_stage}로 변경합니다.");
                m_save_manager.Player.m_max_clear_stage++;
            }

            m_save_manager.SaveData();
            
        }

        public void Recover()
        {
            SaveManager.Instance.Player.m_player_status.m_stamina = SaveManager.Instance.Player.m_player_status.m_max_stamina;
            Debug.Log($"플레이어 체력 회복 : {SaveManager.Instance.Player.m_player_status.m_stamina}");
        }

        public void Finish()
        {
            GameStatus = "Finish";

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        private void AbleUI()
        {
            Debug.Log($"인게임 UI 활성화");
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
            // 인게임 내 환경설정 버튼
            // HP, MP 상황?
            // 이거 비활성화까지
        }

        private void DisableUI()
        {
            Debug.Log($"인게임 UI 비활성화");
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
            // 인게임 내 환경설정 버튼
            // HP, MP 상황?
            // 이거 비활성화까지
        }

        public void ReturnEnemy()
        {
            EnemyFactory enemy_factory = FindAnyObjectByType<EnemyFactory>();
            EnemyCtrl[] m_enemies = FindObjectsByType<EnemyCtrl>(FindObjectsSortMode.None);

            foreach(var enemy in m_enemies)
            {
                enemy_factory.OnReturnEnemy(enemy);
            }
        }
    }
}
