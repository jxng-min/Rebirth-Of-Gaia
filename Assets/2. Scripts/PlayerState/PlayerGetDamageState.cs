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

        private float m_damage ;
        private Vector2 m_enemy_vector;

        private SaveManager m_save_manager;

        private void Start()
        {
            m_save_manager=GameObject.FindAnyObjectByType<SaveManager>();
        }


        public void Init(float damage, Vector2 EnemyVector)
        {
            this.m_damage = damage;
            this.m_enemy_vector = EnemyVector;
        }

        public void Handle(PlayerCtrl player_ctrl)
        {
            if (!m_player_ctrl)
            {
                m_player_ctrl = player_ctrl;
                m_player_object = m_player_ctrl.gameObject;
                m_player_sprite = m_player_object.GetComponent<SpriteRenderer>();
            }

            
            m_save_manager.Player.m_player_status.m_stamina-=m_damage;
            Debug.Log($"플레이어가 {m_damage} 데미지를 받음");
            Debug.Log($"현재 체력 : {m_save_manager.Player.m_player_status.m_stamina}");
            m_player_object.layer = 12; // UnDamaging Player로 레이어 변경
            m_player_sprite.color = new Color(1, 1, 1, 0.4f);

            StartCoroutine(m_player_ctrl.PlayerGetKnockBack(m_enemy_vector)); // 플레이어 넉백 호출

            Invoke("OnDamaged", 1.3f);
        }


        public void OnDamaged() // 다시 데미지를 받을 수 있는 레이어로 변경
        {
            m_player_object.layer = 8; //원래의 Player 레이어로 변경
            m_player_sprite.color = new Color(1, 1, 1, 1);
        }
    }
}


