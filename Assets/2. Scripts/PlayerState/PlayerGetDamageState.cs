using Junyoung;
using UnityEngine;

namespace Junyoung
{
    public class PlayerGetDamageState : MonoBehaviour,IPlayerState
{
    private PlayerCtrl m_player_ctrl;

    public void Handle(PlayerCtrl player_ctrl)
    {
        if (!m_player_ctrl)
        {
            m_player_ctrl = player_ctrl;
        }

        
    }
}
}

