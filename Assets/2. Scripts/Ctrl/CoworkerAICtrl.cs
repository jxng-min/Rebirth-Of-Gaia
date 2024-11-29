using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using Junyoung;
using System.Collections.Generic;
using Jongmin;

public class CoworkerAICtrl : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField]
    private List<GameObject> m_enemies;
    [SerializeField]
    private Transform m_target;
    [SerializeField]
    private float m_activate_distance = 10f;
    [SerializeField]
    private float m_path_update_seconds = 0.5f;

    [Header("Position Check")]
    [SerializeField]
    private float m_stuck_check_interval = 1f;
    [SerializeField]
    private float m_stuck_threshold = 0.1f;
    private Vector2 m_last_position;
    private float m_position_check_timer = 0f;

    [Header("Physics")]
    [SerializeField]
    private float m_speed = 150f;
    [SerializeField]
    private float m_next_waypoint_distance = 0.2f;
    [SerializeField]
    private float m_jump_node_height_requirement = 0.3f;
    [SerializeField]
    private float m_jump_check_offset = 0.1f;

    private bool m_follow_enabled = true;
    private bool m_jump_enabled = true;
    private bool m_direction_look_enabled = true;

    private Path m_path;
    private int m_current_waypoint = 0;
    private bool m_is_grounded = false;
    private Seeker m_seeker;
    private Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_seeker = GetComponent<Seeker>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            m_enemies.Add(enemies[i]);
        }

        FindMinDistanceEnemy();

        m_last_position = transform.position;
        InvokeRepeating("UpdatePath", 0f, m_path_update_seconds);
    }

    private void Update()
    {
        if(GameManager.Instance.GameStatus == "Playing")
        {
            if (m_target == null)
            {
                if (m_enemies.Count <= 0)
                {
                    Debug.Log("모든 적을 처치했기 때문에 동작을 멈춥니다.");
                    return;
                }
                else
                {
                    Debug.Log("타겟이 없기 때문에 타겟을 찾는 중입니다.");
                    FindMinDistanceEnemy();
                }
            }

            m_position_check_timer += Time.deltaTime;
            if (m_position_check_timer >= m_stuck_check_interval)
            {
                float distance_moved = Vector2.Distance(transform.position, m_last_position);

                if (distance_moved < m_stuck_threshold)
                {
                    Debug.Log("캐릭터가 길이 막혀서 이동하지 못하고 있습니다. 경로를 재탐색합니다.");
                    ResetPathfinding();
                }

                m_last_position = transform.position;
                m_position_check_timer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.GameStatus == "Playing")
        {
            if(m_target != null)
            {
                if (TargetReached())
                {
                    int delete_target_index = 0;

                    Debug.Log($"{m_target.name} 타겟 추적에 성공하였습니다.");
                    for(int i = 0; i < m_enemies.Count; i++)
                    {
                        Debug.Log($"m_enemies[i]의 이름: {m_enemies[i].name}");
                        if(m_target.name == m_enemies[i].name)
                        {
                            delete_target_index = i;
                        }
                    }

                    Debug.Log($"{m_target.name}을 공격하여 파괴합니다.");
                    m_target = null;
                    m_enemies.RemoveAt(delete_target_index);

                    m_follow_enabled = false;
                    m_rigidbody.linearVelocity = Vector2.zero;
                }
                else
                {
                    m_follow_enabled = true;
                }
            }

            if(m_target != null)
            {
                if (m_follow_enabled && TargetInDistance())
                {
                    PathFollow();
                }
            }
        }
    }

    private void UpdatePath()
    {
        if(m_follow_enabled && TargetInDistance() && m_seeker.IsDone())
        {
            m_seeker.StartPath(m_rigidbody.position, m_target.position, OnPathComplete);
        }
    }

    private bool TargetReached()
    {
        float target_distance = 0.8f;

        return Vector2.Distance(transform.position, m_target.position) <= target_distance;
    }

    private void PathFollow()
    {
        if (m_path == null || m_current_waypoint >= m_path.vectorPath.Count)
        {
            return;
        }

        m_is_grounded = Physics2D.Raycast(
                                            transform.position, 
                                            Vector3.down, 
                                            GetComponent<Collider2D>().bounds.extents.y + m_jump_check_offset
                                         );

        Vector2 direction = ((Vector2)m_path.vectorPath[m_current_waypoint] - m_rigidbody.position).normalized;

        if (m_jump_enabled && m_is_grounded && direction.y > m_jump_node_height_requirement)
        {
            Debug.Log("동료가 점프합니다.");
            m_is_grounded = false;
            m_rigidbody.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        }

        m_rigidbody.linearVelocity = new Vector2(direction.x * m_speed * Time.fixedDeltaTime, m_rigidbody.linearVelocity.y);

        if (Vector2.Distance(m_rigidbody.position, m_path.vectorPath[m_current_waypoint]) < m_next_waypoint_distance)
        {
            m_current_waypoint++;
        }

        if (m_direction_look_enabled)
        {
            if((direction.x < 0f))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if((direction.x > 0f))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, m_target.transform.position) < m_activate_distance;
    }

    private void OnPathComplete(Path p)
    {
        m_path = p;
        m_current_waypoint = 0;
    }

    private void FindMinDistanceEnemy()
    {
        float min_distance = m_activate_distance;
        int index = 0;

        for(int i = 0; i < m_enemies.Count; i++)
        {
            if(Vector2.Distance(transform.position, m_enemies[i].transform.position) < min_distance)
            {
                min_distance = Vector2.Distance(transform.position, m_enemies[i].transform.position);
                index = i;
            }
        }

        m_target = m_enemies[index].GetComponent<Transform>();       
    }

    private void ResetPathfinding()
    {
        m_target = m_enemies[Random.Range(0, m_enemies.Count)].GetComponent<Transform>();
    }
}