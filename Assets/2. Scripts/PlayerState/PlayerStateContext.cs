using UnityEngine;

namespace Junyoung
{
    // PlayerCtrl에서 이 클래스의 객체를 사용해서 플레이어의 상태를 관리
    public class PlayerStateContext 
    {
         private readonly PlayerCtrl m_player_ctrl;

        //프로퍼티, get,set으로 멤버를 읽고 쓸 때 조건을 부여하는 기능 get,set에 조건을 적지 않으면 일단 변수와 같은 역할을 함, 캡슐화를 통한 접근 제어
        //(나중에 조건을 추가 가능하기 때문에 프로퍼티로 작성, 유효성 검사나, 나중에 내부 로직을 추가하거나 하는 확장성이 좋음)
        //PlayerStateContext 객체가 관리하고 있는 플레이어의 상태를 저장함(IPlayerState는 상태를 추상화한 인터페이스니까 IPlayerState를 구현한 클래스들을 저장 가능)

        public IPlayerState CurrentState { get; set; }

        //생성자, PlayerStateContext 객체가 생성될 때 호출돼서 PlayerCtrl의 인스턴스를 m_player_ctrl 필드에 할당
        //-> 이 클래스에서 PlayerCtrl 객체를 내부적으로 참조 할 수 있게 됨
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


