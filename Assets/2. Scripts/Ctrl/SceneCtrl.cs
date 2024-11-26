using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jongmin
{
    public class SceneCtrl : MonoBehaviour
    {
        [SerializeField]
        private string m_scene_name;

        // m_scene_name 씬으로 이동하는 메소드
        public void ReplaceScene()
        {
            Debug.Log(m_scene_name + "으로 씬을 전환합니다.");
            
            SceneManager.LoadScene(m_scene_name);
        }

        // Title에서 PlayerSelect와 Game 중 이동할 곳을 결정하는 메소드
        public void TitleReplaceScene()
        {
            if(File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            {
                Debug.Log("이미 캐릭터를 생성한 기록이 있습니다.");
                SceneManager.LoadScene("Game");
            }
            else
            {
                Debug.Log("캐릭터를 생성한 기록이 없습니다.");
                SceneManager.LoadScene("PlayerSelect");
            }
        }
    }
}