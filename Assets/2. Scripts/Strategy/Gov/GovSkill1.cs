using UnityEngine;

public class GovSkill1: Skill
{
    public float m_skill_cool_time { get; set; }

    public void Effect()
    {
        Debug.Log("거브가 내부 정보 활용을 사용한다.");
    }
}
