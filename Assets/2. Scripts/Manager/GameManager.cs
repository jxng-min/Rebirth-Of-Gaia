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

            StartCoroutine(FindPlayer());

            m_player_ctrl.GetComponent<Animator>().speed = 1f;

            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Setting()
        {
            GameStatus = "Setting";

            // 모든 오브젝트 정지 필요

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

            SoundManager.Instance.StopBGM();
            // 게임오버 효과음

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(true);

            Debug.Log("스테이지 게임오버!");
        }

        public void Clear()
        {
            GameStatus = "Clear";

            

            EnemyFactory enemy_factory = FindAnyObjectByType<EnemyFactory>();
            EnemyCtrl[] m_enemies = FindObjectsByType<EnemyCtrl>(FindObjectsSortMode.None);
            foreach(var enemy in m_enemies)
            {
                enemy_factory.OnReturnEnemy(enemy);
            }
            
            SoundManager.Instance.StopBGM();
            // 클리어 효과음

            SeedCtrl seed = FindAnyObjectByType<SeedCtrl>();
            Destroy(seed);

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);

            Debug.Log("스테이지 클리어!");
        }

        public void Finish()
        {
            GameStatus = "Finish";

            DisableUI();
            GameObject.Find("Panels").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Conquer()
        {
            GameStatus = "Conquer";

            //마지막 적에게서 씨앗을 소환하는 로직
        }

        private void DisableUI()
        {
            GameObject joystick = GameObject.Find("Joystick");
            //GameObject menu_button = GameObject.Find("MenuButton");
            GameObject button_ui = GameObject.Find("Button UI");
            //GameObject level_ui = GameObject.Find("Level UI");

            joystick.SetActive(false);
            //menu_button.SetActive(false);
            button_ui.SetActive(false);
            //level_ui.SetActive(false);
        }

        private IEnumerator FindPlayer()
        {
            float target_time = 3f;
            float elapsed_time = 0f;

            while(elapsed_time < target_time)
            {
                elapsed_time += Time.deltaTime;

                m_player_ctrl = FindAnyObjectByType<PlayerCtrl>();

                if(m_player_ctrl)
                {
                    yield break;
                }

                yield return null;
            }
        }
    }
}
