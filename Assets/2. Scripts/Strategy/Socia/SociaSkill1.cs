using Jongmin;
using System.Collections;
using UnityEngine;

public class SociaSkill1 : MonoBehaviour,Skill
{
    // 효과 : 본인의 공격력과 방어력을 10초간 20% 상승
    // 발동 조건 : 사회력 20 이상
    // 쿨타임 3초
    private SkillCoolDownCtrl m_skill_cool_cown_ctrl;
    private float m_skill_cool_time = 10f;
    private float m_skill_duration = 5f;
    private float m_increase_value = 0.2f;

    public void Effect()
    {
        if(!m_skill_cool_cown_ctrl)
        {
            m_skill_cool_cown_ctrl = gameObject.AddComponent<SkillCoolDownCtrl>();
        }

        if(m_skill_cool_cown_ctrl.m_is_ready)
        {
            Debug.Log("소셔가 연대의 외침을 사용한다.");
            StartCoroutine(GetStrength(m_skill_duration));
            StartCoroutine(m_skill_cool_cown_ctrl.CoolDownCoroutine(m_skill_cool_time));
        }
        else
        {
            Debug.Log("연대의 외침이 준비되지 않았다.");
        }
    }

    private IEnumerator GetStrength(float time)
    {
        float increase_strength = SaveManager.Instance.Player.m_player_status.m_strength * m_increase_value;
        SaveManager.Instance.Player.m_player_status.m_strength += increase_strength;
        Debug.Log($"소셔의 공격력이 20%상승 : {SaveManager.Instance.Player.m_player_status.m_strength}");
        yield return new WaitForSeconds(time);
        SaveManager.Instance.Player.m_player_status.m_strength -= increase_strength;
        Debug.Log($"소셔의 공격력이 다시 감소 :{SaveManager.Instance.Player.m_player_status.m_strength}");
    }

}
