using Jongmin;
using Junyoung;
using UnityEngine;

namespace Junyoung
{
    public class PlayerGetDamageState : MonoBehaviour,IPlayerState
    {
        private PlayerCtrl m_player_ctrl;
        private GameObject m_player_object;
        private SpriteRenderer m_player_sprite;

        public float Damage { get; set; }
        public Vector2 EnemyVector { get; set;}

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;
                m_player_object = m_player_ctrl.gameObject;
                m_player_sprite = m_player_object.GetComponent<SpriteRenderer>();
            }

            SaveManager.Instance.Player.m_player_status.m_stamina -= Damage;
            Debug.Log($"플레이어가 {Damage} 데미지를 받음");
            Debug.Log($"현재 체력 : {SaveManager.Instance.Player.m_player_status.m_stamina}");

            if(SaveManager.Instance.Player.m_player_status.m_stamina <= 0f)
            {
                Debug.Log($"플레이어 체력이 0 이하, 사망");
                GameEventBus.Publish(GameEventType.DEAD);
            }

            m_player_object.layer = 12;
            m_player_sprite.color = new Color(1f, 1f, 1f, 0.4f);

            StartCoroutine(m_player_ctrl.PlayerGetKnockBack(EnemyVector));
            m_player_ctrl.GetComponent<Animator>().SetTrigger("GetDamage");

            Invoke("OnDamaged", 2f);
        }

        public void OnDamaged()
        {
            m_player_object.layer = 8;
            m_player_sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}