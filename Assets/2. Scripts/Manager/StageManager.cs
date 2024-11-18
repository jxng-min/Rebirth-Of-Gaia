using System.Collections;
using System.Collections.Generic;
using Junyoung;
using UnityEngine;


namespace Jongmin
{
    public class StageManager : Singleton<StageManager>
    {
        private Dictionary<string, Vector3> m_portal_datas;
        public string[] m_portals;
        public Vector3[] m_player_positions;

        public float m_wait_time = 2.0f;

        private void Start()
        {
            if(m_portals.Length != m_player_positions.Length)
            {
                Debug.Log("포탈과 위치 중 입력되지 않은 값이 존재합니다.");
            }
            
            m_portal_datas = new Dictionary<string, Vector3>();
            for(int i = 0; i < m_portals.Length; i++)
            {
                m_portal_datas[m_portals[i]] = m_player_positions[i];
            }
        }

        private IEnumerator InteractWithPortalCoroutine(string portal_name)
        {
            PlayerCtrl player_ctrl = FindAnyObjectByType<PlayerCtrl>();

            if (player_ctrl == null)
            {
                Debug.LogError("PlayerCtrl 객체를 찾을 수 없습니다.");
                yield break;
            }

            float current_wait_time = 0;
            while (current_wait_time < m_wait_time)
            {
                if (player_ctrl.m_is_tunneling)
                {
                    current_wait_time += Time.deltaTime;
                    yield return null;
                }
                else
                {
                    Debug.Log("플레이어가 포탈 이동을 취소하였습니다.");
                    yield break;
                }
            }

            if (m_portal_datas.ContainsKey(portal_name))
            {
                player_ctrl.m_rigidbody.MovePosition(player_ctrl.transform.position + m_portal_datas[portal_name]);
                Debug.Log($"{portal_name} 포탈로 이동하였습니다.");
            }
            else
            {
                Debug.LogError($"'{portal_name}'에 해당하는 포탈 데이터가 없습니다.");
            }
        }

        public void InteractWithPortal(string portal_name)
        {
            StartCoroutine(InteractWithPortalCoroutine(portal_name));
        }
    }
}