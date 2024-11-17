using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jongmin
{
    public class SceneCtrl : MonoBehaviour
    {
        public static void ReplaceScene(string scene_name)
        {
            SceneManager.LoadScene(scene_name);
        }
    }
}