using UnityEngine;

namespace Junyoung
{
    // 플레이어 상태를 추상화하는 클래스
    public interface IPlayerState
    {
        public void Handle(PlayerCtrl ctrl);
    }
}

