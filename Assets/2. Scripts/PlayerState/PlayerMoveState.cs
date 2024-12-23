using Jongmin;
using System.Collections;
using UnityEngine;

namespace Junyoung
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl) 
            {
                m_player_ctrl = player_ctrl;
            }
            
            if(!m_player_ctrl.IsMove)
            {
                StartCoroutine(MoveSE());
            }
            
            GetComponent<Animator>().SetBool("IsMove", true);
        }

        private IEnumerator MoveSE()
        {
            m_player_ctrl.IsMove = true;

            SoundManager.Instance.PlayEffect($"foot_0{Random.Range(1, 7)}");

            yield return new WaitForSeconds(0.3f);

            m_player_ctrl.IsMove = false;
        }
    }
}