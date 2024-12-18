using Junyoung;
using UnityEngine;

public class SociaSkill2 :MonoBehaviour, Skill
{
    private SkillCoolDownCtrl m_skill_cool_cown_ctrl;
    public float m_skill_cool_time { get; set; } = 25f;

    public void Effect()
    {
        Debug.Log("소셔가 설득의 힘을 사용한다.");
    }
    public void Effect(Collider2D collider)
    {
        if (!m_skill_cool_cown_ctrl)
        {
            m_skill_cool_cown_ctrl = gameObject.AddComponent<SkillCoolDownCtrl>();
        }

        if (m_skill_cool_cown_ctrl.m_is_ready)
        {
            Debug.Log($"소셔가 설득의 힘을 사용하여{collider.name}을 제거한다.");
            collider.GetComponent<EnemyCtrl>().EnemyDead();
            StartCoroutine(m_skill_cool_cown_ctrl.CoolDownCoroutine(m_skill_cool_time));

        }
        else
        {
            Debug.Log($"설득의 힘이 아직 준비되지 않았다.");
        }


    }
}
