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

        private SaveManager m_save_manager;

        private void Start()
        {
            m_save_manager=GameObject.FindAnyObjectByType<SaveManager>();
        }

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;
                m_player_object = m_player_ctrl.gameObject;
                m_player_sprite = m_player_object.GetComponent<SpriteRenderer>();
            }

            m_save_manager.Player.m_player_status.m_stamina -= Damage;
            Debug.Log($"플레이어가 {Damage} 데미지를 받음");
            Debug.Log($"현재 체력 : {m_save_manager.Player.m_player_status.m_stamina}");

            if(m_save_manager.Player.m_player_status.m_stamina <= 0f)
            {
                Debug.Log($"플레이어 체력이 0 미만, 사망");
                GameEventBus.Publish(GameEventType.DEAD);
            }

            m_player_object.layer = 12;
            m_player_sprite.color = new Color(1f, 1f, 1f, 0.4f);

            StartCoroutine(m_player_ctrl.PlayerGetKnockBack(EnemyVector));

            Invoke("OnDamaged", 1.3f);
        }

        public void OnDamaged()
        {
            m_player_object.layer = 8;
            m_player_sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}


