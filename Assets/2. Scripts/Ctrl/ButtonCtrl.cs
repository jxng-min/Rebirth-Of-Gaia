using Junyoung;
using UnityEngine;

namespace Jongmin
{
    public class ButtonCtrl : MonoBehaviour
    {
        private PlayerCtrl m_player_ctrl;

        void Update()
        {
            if (m_player_ctrl == null)
                m_player_ctrl = FindAnyObjectByType<PlayerCtrl>();
        }

        public void LeftArrowClick()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.MoveVector = Vector2.left;
                m_player_ctrl.PlayerMove();
            }
        }

        public void RightArrowClick()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.MoveVector = Vector2.right;
                m_player_ctrl.PlayerMove();
            }
        }

        public void UpArrowClick()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerJump();
            }
        }

        public void DownArrowClick()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.PlayerDown();
            }
        }

        public void ArrowUp()
        {
            m_player_ctrl.MoveVector = Vector2.zero;
            m_player_ctrl.PlayerStop();
        }

        public void Skill1Click()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.m_player_skills[0].Effect();
            }
        }

        public void Skill2Click()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.m_player_skills[1].Effect();
            }
        }

        public void Skill3Click()
        {
            if(GameManager.Instance.GameStatus == "Playing")
            {
                m_player_ctrl.m_player_skills[2].Effect();
            }
        }
    }
}
