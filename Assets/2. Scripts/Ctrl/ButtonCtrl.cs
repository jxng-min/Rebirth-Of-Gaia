using Junyoung;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


namespace Jongmin
{
    public class ButtonCtrl : MonoBehaviour
    { 
        [Header("Hide Skill Images")]
        [SerializeField] private Image[] m_hide_images;

        private PlayerCtrl m_player_ctrl;



        public void TitleClick()
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            {
                Debug.Log("이미 캐릭터를 생성한 기록이 있습니다.");
                SceneCtrl.ReplaceScene("Game");
            }
            else
            {
                Debug.Log("캐릭터를 생성한 기록이 없습니다.");
                SceneCtrl.ReplaceScene("Intro");
            }
        }

        public void IntroClick()
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            {
                SceneCtrl.ReplaceScene("Game");
            }
            else
            {
                SceneCtrl.ReplaceScene("PlayerSelect");
            }
        }

        public void PlayClick()
        {
            SceneCtrl.ReplaceScene("Game");
        }

        public void UpArrowClick()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerJump();
            }
        }

        public void DownArrowClick()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerDown();
            }
        }

        public void ArrowUp()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.PlayerStop();
        }

        public void Skill1Click()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerUseSkill1();
                StartCoroutine(CoolDownImageCorutine(m_hide_images[0], m_player_ctrl.m_player_skills[0].m_skill_cool_time));
            }
        }

        public void Skill2Click()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerUseSkill2();
                StartCoroutine(CoolDownImageCorutine(m_hide_images[1], m_player_ctrl.m_player_skills[1].m_skill_cool_time));

            }
        }

        public void Skill3Click()
        {
            if (m_player_ctrl == null)
            {
                Debug.Log("PlayerCtrl이 없으므로 태그로 찾아보고 있습니다.");
                m_player_ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                Debug.Log("PlayerCtrl을 찾는 데 성공했습니다.");
            }

            if (GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerUseSkill3();
                StartCoroutine(CoolDownImageCorutine(m_hide_images[2], m_player_ctrl.m_player_skills[2].m_skill_cool_time));

            }
        }
        public IEnumerator CoolDownImageCorutine(Image image, float cool_time)
        {
            image.gameObject.SetActive(true);
            float now_time = cool_time;
            while (now_time > 0)
            {
                now_time -= Time.deltaTime;
                image.fillAmount = now_time / cool_time;
                yield return null;
            }
            image.fillAmount = 0f;
            image.gameObject.SetActive(false);          
        }
    }
}
