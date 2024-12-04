using Jongmin;
using Junyoung;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Header("SaveManager")]
    [SerializeField]
    private SaveManager m_save_manager;

    [Header("SpawnEnemy")]
    [SerializeField]
    private List<EnemyStatus> m_enemy_status_list;
    [SerializeField]
    private List<GameObject> m_enemy_prefab;
    public Vector3[] m_enemy_spawn_pos;

    public void SpawnEnemy(EnemyType type, int transform_index)
    {
        var newEnemy = Instantiate(m_enemy_prefab[(int)type]).GetComponent<EnemyCtrl>(); // 생성된 객체의 스크립트를 가져옴
        newEnemy.m_enemy_status = m_enemy_status_list[m_save_manager.Player.m_stage_id];
        newEnemy.name = newEnemy.m_enemy_status.EnemyName;
        Debug.Log($"{type}타입의 적 생성위치{transform_index}에서 생성");
        newEnemy.transform.position = m_enemy_spawn_pos[transform_index];

    }

}
