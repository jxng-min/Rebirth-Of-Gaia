using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jongmin
{
    public class SceneCtrl : MonoBehaviour
    {
        public string m_scene_name;
        public void ReplaceScene()
        {
            Debug.Log(m_scene_name + "으로 씬을 전환합니다.");
            
            SceneManager.LoadScene(m_scene_name);
        }

        public void TitleReplaceScene()
        {
            if(File.Exists(Application.persistentDataPath + "/PlayerData.txt"))
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