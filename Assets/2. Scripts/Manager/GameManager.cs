using Unity.Loading;
using UnityEngine;

namespace Jongmin
{
public class GameManager : Singleton<GameManager>
{
        public string m_game_status;
        public Character CharacterType{ get; set; }

        private void Start()
        {
            GameEventBus.Subscribe(GameEventType.NONE, None);
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
            m_game_status = "None";
        }

        public void Loading()
        {
            m_game_status = "Loading";
        }

        public void Playing()
        {
            m_game_status = "Playing";
        }

        public void Setting()
        {
            m_game_status = "Setting";
        }

        public void Dead()
        {
            m_game_status = "Dead";
        }

        public void Finish()
        {
            m_game_status = "Finish";
        }
    }
}
