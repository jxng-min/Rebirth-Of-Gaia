using Junyoung;
using UnityEngine;
using UnityEngine.UI;

namespace Jongmin
{
    public class ObjectScanCtrl : MonoBehaviour
    {
        private Junyoung.PlayerCtrl m_player_ctrl;
        private Vector3 m_player_direction_vector;
        private GameObject m_scan_object;

        private void Start()
        {
            m_player_ctrl = GetComponent<Junyoung.PlayerCtrl>();
        }

        private void Update()
        {
            SetPlayerMoveDirection();
        }

        private void FixedUpdate()
        {
            MakeRayToCheckObject();
            MakeRayToJump();
        }

        // 오브젝트 상호작용 결정 레이를 발사할 방향을 결정하는 메소드
        private void SetPlayerMoveDirection()
        {
            if(m_player_ctrl.m_move_vec.x != 0f)
            {
                if(m_player_ctrl.m_move_vec.x >= 0f)
                {
                    m_player_direction_vector = Vector3.right;
                }
                else
                {
                    m_player_direction_vector = Vector3.left;
                }
            }
        }

        // 상호작용 행동을 결정하는 메소드
        public void SelectInteractBehavior(Button button)
        {
            if(button.name == "Talk Button"
                                        && GameManager.Instance.m_game_status == "Playing"
                                        && m_scan_object != null)
                                                
            {
                Debug.Log($"{m_scan_object.name}과 대화를 시작합니다.");

            }

            if(button.name == "Jump Button"
                                            && GameManager.Instance.m_game_status == "Playing"
                                            && !m_player_ctrl.m_is_jump)
            {
                Debug.Log("플레이어가 점프를 합니다.");
                GetComponent<PlayerCtrl>().PlayerJump();
            }
        }

        // 플레이어가 오브젝트와 상호작용을 할 수 있는 거리를 확인하는 메소드
        private void MakeRayToCheckObject()
        {
            RaycastHit2D ray_hit = Physics2D.Raycast(
                                                        m_player_ctrl.m_rigidbody.position,
                                                        m_player_direction_vector,
                                                        1.0f,
                                                        LayerMask.GetMask("OBJECT")
                                                    );

            if (ray_hit.collider)
            {
                Debug.DrawRay(m_player_ctrl.m_rigidbody.position, m_player_direction_vector * 1.0f, new Color(0, 1, 0));
                m_scan_object = ray_hit.collider.gameObject;
            }
            else
            {
                Debug.DrawRay(m_player_ctrl.m_rigidbody.position, m_player_direction_vector * 1.0f, new Color(1, 0, 0));
                m_scan_object = null;
            }
        }

        // 플레이어가 점프를 할 수 있는지 땅과의 거리를 확인하는 메소드
        private void MakeRayToJump()
        {
            Vector3 left_down_dir = new Vector3(-1,-1,0).normalized;
            Vector3 right_down_dir = new Vector3(1, -1, 0).normalized;

            RaycastHit2D left_ray_hit = Physics2D.Raycast(
                                                            m_player_ctrl.m_rigidbody.position + Vector2.down,
                                                            left_down_dir,
                                                            1.0f,
                                                            LayerMask.GetMask("GROUND")
                                                         );
            RaycastHit2D right_ray_hit = Physics2D.Raycast(
                                                            m_player_ctrl.m_rigidbody.position + Vector2.down,
                                                            right_down_dir,
                                                            1.0f,
                                                            LayerMask.GetMask("GROUND")
                                                          );

            if (left_ray_hit.collider || right_ray_hit.collider)
            {
                Debug.DrawRay(m_player_ctrl.m_rigidbody.position + Vector2.down, left_down_dir * 1.0f, new Color(0, 1, 0));
                Debug.DrawRay(m_player_ctrl.m_rigidbody.position + Vector2.down, right_down_dir * 1.0f, new Color(0, 1, 0));
                m_player_ctrl.m_is_jump = false;
            }
            else
            {
                Debug.DrawRay(m_player_ctrl.m_rigidbody.position + Vector2.down, left_down_dir * 1.0f, new Color(1, 0, 0));
                Debug.DrawRay(m_player_ctrl.m_rigidbody.position + Vector2.down, right_down_dir * 1.0f, new Color(1, 0, 0));
                m_player_ctrl.m_is_jump = true;
            }
        }
    }
}
