using Junyoung;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Data", menuName ="Scriptable Object/Enemy Status", order =int.MaxValue)]
public class EnemyStatus : ScriptableObject
{
    [SerializeField]
    private EnemyType m_enemy_type;
    public EnemyType EnemyType { get { return m_enemy_type; } set { m_enemy_type = value; } }

    [SerializeField]
    private float m_enemy_hp;
    public float EnemyHP { get { return m_enemy_hp; } set { m_enemy_hp = value; } }

    [SerializeField]
    private float m_enemy_move_speed;
    public float EnemyMoveSpeed { get { return m_enemy_move_speed; } set { m_enemy_move_speed = value; } }

    [SerializeField]
    private float m_enemy_damage;
    public float EnemyDamage { get { return m_enemy_damage; } set { m_enemy_damage = value; } }

    [SerializeField]
    private float m_enemy_attack_delay;
    public float EnemyAttackDelay { get { return m_enemy_attack_delay; } set { m_enemy_attack_delay = value; } }

    [SerializeField]
    private int m_enemy_ex;
    public int EnemyEx { get { return m_enemy_ex; } set { m_enemy_ex= value; } }
}
