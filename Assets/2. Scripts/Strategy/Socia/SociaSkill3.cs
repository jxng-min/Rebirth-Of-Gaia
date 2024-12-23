using Jongmin;
using UnityEngine;

public class SociaSkill3 :MonoBehaviour, Skill
{
    public float m_skill_cool_time { get; set; } = 20f;
    private float m_heal_value = 0.3f;
    public void Effect()
    {
        Debug.Log("소셔가 희망의 결속을 사용한다.");

        float heal = SaveManager.Instance.Player.m_player_status.m_max_stamina * m_heal_value;
        if (SaveManager.Instance.Player.m_player_status.m_stamina + heal > SaveManager.Instance.Player.m_player_status.m_max_stamina)
        {
            SaveManager.Instance.Player.m_player_status.m_stamina = SaveManager.Instance.Player.m_player_status.m_max_stamina;
            Debug.Log($"현재 체력 : {SaveManager.Instance.Player.m_player_status.m_stamina}.");
        }
        else
        {
            SaveManager.Instance.Player.m_player_status.m_stamina += heal;
            Debug.Log($"현재 체력 : {SaveManager.Instance.Player.m_player_status.m_stamina}.");
        }       
    }
}
