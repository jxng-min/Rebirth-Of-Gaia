using UnityEngine;

public class EnvaSkill1 : Skill
{
    public float m_skill_cool_time { get; set; }

    public void Effect()
    {
        Debug.Log("엔바가 오염 정화를 사용한다.");
    }
}
