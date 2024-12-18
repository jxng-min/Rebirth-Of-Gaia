using UnityEngine;

public class GovSkill3 : Skill
{
    public float m_skill_cool_time { get; set; }

    public void Effect()
    {
        Debug.Log("거브가 혼란 유발을 사용한다.");
    }
}
