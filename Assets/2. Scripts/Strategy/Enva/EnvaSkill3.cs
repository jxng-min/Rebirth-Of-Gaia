using UnityEngine;

public class EnvaSkill3 : Skill
{
    public float m_skill_cool_time { get; set; }

    public void Effect()
    {
        Debug.Log("엔바가 환경 기반 방어를 사용한다.");
    }
}
