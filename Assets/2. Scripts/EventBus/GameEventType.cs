using UnityEngine;

namespace Jongmin
{
    // 게임 상태를 나타낼 열거형
    public enum GameEventType
    {
        NONE = 0,
        LOADING = 1,
        PLAYING = 2,
        SETTING = 3,
        DEAD = 4,
        FINISH = 5,
    }
}
