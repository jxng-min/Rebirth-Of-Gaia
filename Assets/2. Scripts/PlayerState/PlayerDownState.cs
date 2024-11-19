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

            Debug.Log("내려감");
            GetComponent<PlatformScanCtrl>().StartDisableCollision();
        }
    }
}


