using Jongmin;
using UnityEngine;

public class SociaSkill3 :MonoBehaviour, Skill
{
    private SkillCoolDownCtrl m_skill_cool_cown_ctrl;
    public float m_skill_cool_time { get; set; } = 20f;
    private float m_heal_value = 0.3f;
    private float m_max_stamina = 0;
    public void Effect()
    {
        if (!m_skill_cool_cown_ctrl)
        {
            m_skill_cool_cown_ctrl = gameObject.AddComponent<SkillCoolDownCtrl>();
        }

        if(m_max_stamina < SaveManager.Instance.Player.m_player_status.m_stamina)
        {
            m_max_stamina = SaveManager.Instance.Player.m_player_status.m_stamina;
        }

        if (m_skill_cool_cown_ctrl.m_is_ready)
        {
            Debug.Log("소셔가 희망의 결속을 사용한다.");
            StartCoroutine(m_skill_cool_cown_ctrl.CoolDownCoroutine(m_skill_cool_time));

            float heal = m_max_stamina * m_heal_value;
            if (SaveManager.Instance.Player.m_player_status.m_stamina + heal > m_max_stamina)
            {
                SaveManager.Instance.Player.m_player_status.m_stamina = m_max_stamina;
                Debug.Log($"현재 체력 : {SaveManager.Instance.Player.m_player_status.m_stamina}.");
            }
            else
            {
                SaveManager.Instance.Player.m_player_status.m_stamina += heal;
                Debug.Log($"현재 체력 : {SaveManager.Instance.Player.m_player_status.m_stamina}.");
            }
        }
        else
        {
            Debug.Log("희망의 결속이 준비되지 않았다.");
        }
    }
}
