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

            Debug.Log("플레이어 Down State : 플레이어가 GROUND 플랫폼에서 내려갑니다.");
            GetComponent<PlatformScanCtrl>().StartDisableCollision();
        }
    }
}


