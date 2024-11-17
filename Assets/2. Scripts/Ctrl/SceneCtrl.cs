using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jongmin
{
    public class SceneCtrl : MonoBehaviour
    {
        public static void ReplaceScene(string scene_name)
        {
            Debug.Log(scene_name + "으로 씬을 전환합니다.");
            
            SceneManager.LoadScene(scene_name);
        }
    }
}