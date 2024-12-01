using System.Collections;
using UnityEngine;

namespace Junyoung
{
    public class PlayerDownState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;            
            }

            if(GetComponent<PlatformScanCtrl>().CurrentPlatform)
            {
                m_player_ctrl.IsDown = true;
                GetComponent<Animator>().SetTrigger("Down");
                GetComponent<PlatformScanCtrl>().StartDisableCollision();
                StartCoroutine(Downing());
            }
        }

        private IEnumerator Downing()
        {
            yield return new WaitForSeconds(1f);
            m_player_ctrl.IsDown = false;
        }
    }
}