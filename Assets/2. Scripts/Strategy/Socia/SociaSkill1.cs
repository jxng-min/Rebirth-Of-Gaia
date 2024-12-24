using Jongmin;
using System.Collections;
using UnityEngine;

public class SociaSkill1 : MonoBehaviour,Skill
{
    public float m_skill_cool_time { get; set; } = 30f;
    private float m_skill_duration = 10f;
    private float m_increase_value = 0.2f;
    public void Effect()
    {
         Debug.Log("소셔가 연대의 외침을 사용한다.");
         StartCoroutine(GetStrength(m_skill_duration));
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
