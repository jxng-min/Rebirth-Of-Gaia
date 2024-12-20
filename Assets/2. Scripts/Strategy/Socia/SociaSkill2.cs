using Junyoung;
using UnityEngine;

public class SociaSkill2 :MonoBehaviour, Skill
{
    public float m_skill_cool_time { get; set; } = 25f;
    public void Effect()
    {
        Debug.Log("소셔가 설득의 힘을 사용한다.");
    }
    public void Effect(Collider2D collider)
    {
        Debug.Log($"소셔가 설득의 힘을 사용하여{collider.name}을 제거한다.");
        collider.GetComponent<EnemyCtrl>().EnemyDead();
    }
}
