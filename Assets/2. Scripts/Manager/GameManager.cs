using Junyoung;
using System.Collections;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEngine;

namespace Jongmin
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerCtrl m_player_ctrl;

        public string GameStatus { get; private set; }
        public Character CharacterType{ get; set; }

        private void Start()
        {
            GameEventBus.Subscribe(GameEventType.NONE, None);
            GameEventBus.Subscribe(GameEventType.LOADING, Loading);
            GameEventBus.Publish(GameEventType.NONE);

            SoundManager.Instance.PlayeBGM("Title");
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
        }

        public void Loading()
        {
            GameStatus = "Loading";
        }

        public void Playing()
        {
            GameStatus = "Playing";

            m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 1f;

            AbleUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Setting()
        {
            GameStatus = "Setting";

            SoundManager.Instance.PlayeBGM("Title");

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Dead()
        {
            GameStatus = "Dead";

            m_player_ctrl.PlayerDead();

            EnemyFactory enemy_factory = FindAnyObjectByType<EnemyFactory>();
            EnemyCtrl[] m_enemies = FindObjectsByType<EnemyCtrl>(FindObjectsSortMode.None);
            foreach(var enemy in m_enemies)
            {
                enemy_factory.OnReturnEnemy(enemy);
            }

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 0f;

            SoundManager.Instance.StopBGM();
            // 게임오버 효과음

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
            // 클리어 효과음

            SeedCtrl seed = FindAnyObjectByType<SeedCtrl>();
            Destroy(seed);

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.GetComponent<Animator>().speed = 0f;

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
    }
}
