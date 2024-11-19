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


        private void OnCollisionEnter2D(Collision2D collision) // 플랫폼을 감지하는 매서드
        {
            if (collision.gameObject.CompareTag("PLATFORM"))
            {
                m_current_platform = collision.gameObject;
            }
            
        }

        private void OnCollisionExit2D(Collision2D collision) // 플랫폼의 콜리더에서 벗어났을 때 작동하는 매서드
        {
            m_current_platform = null;
        }


        private IEnumerator DisableCollision()
        {
            BoxCollider2D platform_collider = m_current_platform.GetComponent<BoxCollider2D>();

            Physics2D.IgnoreCollision(m_player_collider, platform_collider);
            yield return new WaitForSeconds(0.25f);
            Physics2D.IgnoreCollision(m_player_collider, platform_collider,false);

        }

        public void StartDisableCollision()
        {
            StartCoroutine(DisableCollision());
        }
        

    }


}

