using UnityEngine;

public class GovSkill2: Skill
{
    public float m_skill_cool_time { get; set; }

    public void Effect()
    {
        Debug.Log("거브가 자원 조달을 사용한다.");
    }
}
