using Unity.Loading;
using UnityEngine;

namespace Jongmin
{
public class GameManager : Singleton<GameManager>
{
        public string GameStatus { get; private set; }
        public Character CharacterType{ get; set; }

        private void Start()
        {
            GameEventBus.Subscribe(GameEventType.NONE, None);
            GameEventBus.Publish(GameEventType.NONE);

            // SoundManager.Instance.PlayeBGM("Title");
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
        }

        public void Setting()
        {
            GameStatus = "Setting";
        }

        public void Dead()
        {
            GameStatus = "Dead";
        }

        public void Finish()
        {
            GameStatus = "Finish";
        }
    }
}
