using System.Collections;
using UnityEngine;

namespace Junyoung
{
    public class PlatformScanCtrl : MonoBehaviour
    {
        [SerializeField] private GameObject m_current_platform;
        private BoxCollider2D m_player_collider;
        private PlayerCtrl m_player_ctrl;

        private void Start()
        {
            m_player_collider = GetComponent<BoxCollider2D>();
            m_player_ctrl = GetComponent<PlayerCtrl>();
        }

        // 플레이어가 뛰어내릴 플랫폼을 감지하는 메소드
        private void OnCollisionEnter2D(Collision2D collision) 
        {
            if (collision.gameObject.CompareTag("PLATFORM"))
            {
                m_current_platform = collision.gameObject;
            }
            
        }

        // 플랫폼의 콜라이더에서 벗어났을 때 작동하는 메소드
        private void OnCollisionExit2D(Collision2D collision)
        {
            m_current_platform = null;
        }


        private IEnumerator DisableCollision()
        {
            if(m_current_platform == null)
            {
                Debug.Log("공중에서는 뛰어 내릴 수 없습니다.");
            }
            else
            {
                BoxCollider2D platform_collider = m_current_platform.GetComponent<BoxCollider2D>();

                Physics2D.IgnoreCollision(m_player_collider, platform_collider);
                yield return new WaitForSeconds(0.25f);
                Physics2D.IgnoreCollision(m_player_collider, platform_collider,false);
            }
        }

        public void StartDisableCollision()
        {
            StartCoroutine(DisableCollision());
        }
    }
}

