using UnityEngine;

namespace Junyoung
{
    [System.Serializable]
    public class StageData 
    {
        public Vector2 m_camera_limit_center;
        public Vector2 m_camera_limit_size;
        public Vector2 m_player_start_position;
        public Vector3 m_enemy_spawn_pos1;
        public Vector3 m_enemy_spawn_pos2;
        public Vector3 m_enemy_spawn_pos3;
        public Vector3 m_enemy_spawn_pos4;

    }

    [System.Serializable]
    public class StageDataWrapper
    {
        public StageData[] StageData;
    }
}

