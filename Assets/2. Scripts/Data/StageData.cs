using UnityEngine;

namespace Junyoung
{
    [System.Serializable] // Unity의 JsonUtility는 직렬화가 된 클래스나 구조체만 JSON으로 변경하거나 로드 할 수 있다
    public class StageData 
    {
        public Vector2 m_camera_limit_center;
        public Vector2 m_camera_limit_size;
        public Vector2 m_player_start_position;

    }

    [System.Serializable]
    public class StageDataWrapper
    {
        public StageData[] StageData; // JSON 배열을 담기 위한 Wrapper 클래스
        //Json이 배열을 지원하지 않으니까 
    }
}

