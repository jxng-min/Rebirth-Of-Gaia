using UnityEngine;

namespace Junyoung
{
    [System.Serializable]
    public class StageData 
    {
        public Vector2 m_camera_limit_center;
        public Vector2 m_camera_limit_size;
        public Vector2 m_player_start_position;

    }

    [System.Serializable]
    public class StageDataWrapper
    {
        public StageData[] StageData;
    }
}

