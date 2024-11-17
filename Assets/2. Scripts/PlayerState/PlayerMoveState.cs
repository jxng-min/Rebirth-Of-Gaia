using UnityEngine;

namespace Junyoung
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl) //상태에 따른 동작을 수행한다
        {
            if (!m_player_ctrl) // 예외처리 null이 아니면 초기화
            {
                m_player_ctrl = player_ctrl;
            }





        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}