using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using Junyoung;

public class CoworkerAICtrl : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform m_target;
    public float m_activate_distance = 50f;
    public float m_path_update_seconds =0.5f;

    [Header("Physics")]
    public float m_speed = 100f;
    public float m_next_waypoint_distance = 3f;
    public float m_jump_node_height_requirement = 0.8f;
    public float m_jump_modifier = 0.3f;
    public float m_jump_check_offset = 0.1f;

    [Header("Custom Behavior")]
    public bool m_follow_enabled = true;
    public bool m_jump_enabled = true;
    public bool m_direction_look_enabled = true;

    private Path m_path;
    private int m_current_waypoint = 0;
    private bool m_is_grounded = false;
    private Seeker m_seeker;
    private Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_seeker = GetComponent<Seeker>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_target = FindAnyObjectByType<PlayerCtrl>().GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, m_path_update_seconds);
    }

    private void FixedUpdate()
    {
        if(m_follow_enabled && TargetInDistance())
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if(m_follow_enabled && TargetInDistance() && m_seeker.IsDone())
        {
            m_seeker.StartPath(m_rigidbody.position, m_target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if(m_path == null)
        {
            return;
        }

        // 경로의 끝에 도달한 경우에 대한 처리
        if(m_current_waypoint >= m_path.vectorPath.Count)
        {
            m_current_waypoint = 0;
            return;
        }

        m_is_grounded = Physics2D.Raycast(transform.position, Vector3.down, GetComponent<Collider2D>().bounds.extents.y + m_jump_check_offset);

        // 방향을 계산하는 로직
        Vector2 direction = ((Vector2)m_path.vectorPath[m_current_waypoint] - m_rigidbody.position).normalized;
        Vector2 force = direction * m_speed * Time.deltaTime;

        // 점프 로직
        if(m_jump_enabled && m_is_grounded)
        {
            if(direction.y > m_jump_node_height_requirement)
            {
                m_rigidbody.AddForce(Vector2.up * m_speed * m_jump_modifier);
            }
        }

        // 이동 로직
        m_rigidbody.AddForce(force);

        // 다음 경로 설정 로직
        float distance = Vector2.Distance(m_rigidbody.position, m_path.vectorPath[m_current_waypoint]);
        if(distance < m_next_waypoint_distance)
        {
            m_current_waypoint++;
        }

        if(m_direction_look_enabled)
        {
            if(m_rigidbody.linearVelocity.x > 0.05f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(m_rigidbody.linearVelocity.x < -0.05f)
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
}