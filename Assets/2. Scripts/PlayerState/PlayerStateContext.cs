using UnityEngine;

namespace Junyoung
{
    public class PlayerStateContext 
    {
        private readonly PlayerCtrl m_player_ctrl;

        public IPlayerState CurrentState { get; set; }

        public PlayerStateContext(PlayerCtrl player_controller)
        {
            m_player_ctrl = player_controller;
        }

        // 현재 상태에 따라 m_player_ctrl 동작을 수행시키는 메소드 
        public void Transition()
        {
            CurrentState.Handle(m_player_ctrl);
        }

        // 상태를 변경하고 변경된 상태에 맞는 동작을 수행시키는 메소드
        public void Transition(IPlayerState state)
        {
            CurrentState = state;
            CurrentState.Handle(m_player_ctrl);
        }
    }
}