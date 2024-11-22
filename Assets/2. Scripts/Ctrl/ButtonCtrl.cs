using Junyoung;
using UnityEngine;

namespace Jongmin
{
    public class ButtonCtrl : MonoBehaviour
    {
        public void LeftArrowClick()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.m_move_vec = Vector2.left;
                player_ctrl.PlayerMove();
            }
        }

        public void RightArrowClick()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.m_move_vec = Vector2.right;
                player_ctrl.PlayerMove();
            }
        }

        public void UpArrowClick()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.PlayerJump();
            }
        }

        public void DownArrowClick()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();
            
            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.PlayerDown();
            }
        }

        public void ArrowUp()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            player_ctrl.m_move_vec = Vector2.zero;
            player_ctrl.PlayerStop();
        }

        public void Skill1Click()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.m_player_skills[0].Effect();
            }
        }

        public void Skill2Click()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.m_player_skills[1].Effect();
            }
        }

        public void Skill3Click()
        {
            PlayerCtrl player_ctrl = GameObject.FindAnyObjectByType<PlayerCtrl>();

            if(GameManager.Instance.m_game_status == "Playing")
            {
                player_ctrl.m_player_skills[2].Effect();
            }
        }
    }
}
