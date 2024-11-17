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
    }
}