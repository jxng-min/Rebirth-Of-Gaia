using UnityEditor.Search;
using UnityEngine;

namespace Junyoung
{
    public class CameraMoveCtrl : MonoBehaviour
    {
        private Transform m_player_transform;
        private float m_camera_move_speed = 1.0f;

        [Header("Camera Setting")]
        [SerializeField]
        private Vector2 m_camera_limit_center;

        public Vector2 CameraLimitCenter 
        { 
            get { return m_camera_limit_center; }
            set { m_camera_limit_center = value; }    
        }
        
        [SerializeField]
        private Vector2 m_camera_limit_size;

        public Vector2 CameraLimitSize 
        { 
            get { return m_camera_limit_size; }
            set { m_camera_limit_size = value; }    
        }

        private float m_camera_height;
        private float m_camera_width;

        void Start()
        {
            m_camera_height = Camera.main.orthographicSize;
            m_camera_width = m_camera_height * Screen.width / Screen.height;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_camera_limit_center, m_camera_limit_size);
        }

        private void Update()
        {
            m_player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    
        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, m_player_transform.position, Time.deltaTime * m_camera_move_speed);

            float lx = m_camera_limit_size.x * 0.5f - m_camera_width;
            float clampX = Mathf.Clamp(transform.position.x , -lx + m_camera_limit_center.x , lx + m_camera_limit_center.x);

            float ly = m_camera_limit_size.y * 0.5f - m_camera_height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + m_camera_limit_center.y, ly + m_camera_limit_center.y);

            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }
}

