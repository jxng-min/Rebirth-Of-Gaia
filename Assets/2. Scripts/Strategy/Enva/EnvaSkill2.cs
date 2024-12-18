using UnityEngine;

public class EnvaSkill2 : Skill
{
    public float m_skill_cool_time { get; set; }

    public void Effect()
    {
        Debug.Log("엔바가 생명 회복을 사용합니다.");
    }
}
