using Jongmin;
using Junyoung;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Junyoung
{
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

        private int m_max_pool_size = 4;

        private Dictionary<EnemyType, IObjectPool<EnemyCtrl> > m_enemy_pools;

        private void Start()
        {
            m_enemy_spawn_pos = new Vector3[4];

            for(int i=0; i<m_enemy_spawn_pos.Length;i++)
            {
                m_enemy_spawn_pos[i] = Vector3.zero;
            }
        }

        private void Awake()
        {
            m_enemy_pools = new Dictionary<EnemyType, IObjectPool<EnemyCtrl>>();

            for(int i =0; i < m_enemy_prefab.Count; i++)
            {
                EnemyType type = (EnemyType)i;

                m_enemy_pools[type] = new ObjectPool<EnemyCtrl>( ()=> CreateEnemy(type), OnGetEnemy, OnReturnEnemy, OnDestoryEnemy, maxSize:m_max_pool_size); // 매개변수 없는 함수를 요하기 때문에 람다식으로 감싸서 매개변수를 넘김
                Debug.Log($"오브젝트 pool 타입 : {type} 초기화 완료.");

            }           
        }
        
        public void SpawnEnemy(EnemyType type, int transform_index)
        {
            var newEnemy = m_enemy_pools[type].Get();
            newEnemy.m_enemy_status = m_enemy_status_list[m_save_manager.Player.m_stage_id];
            newEnemy.name = type.ToString();
            newEnemy.transform.position = m_enemy_spawn_pos[transform_index];
            newEnemy.SetPatrolTime();
            //newEnemy.testEnemyDead();
            Debug.Log($"{type}타입의 적 생성위치{transform_index}에서 생성");
        }
        
        
        private EnemyCtrl CreateEnemy(EnemyType type)
        {
            var newEnemy = Instantiate(m_enemy_prefab[(int)type]).GetComponent<EnemyCtrl>();
            newEnemy.SetEnemyPool(m_enemy_pools[type]);
            return newEnemy;
        }

        private void OnGetEnemy(EnemyCtrl enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        public void OnReturnEnemy(EnemyCtrl enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestoryEnemy(EnemyCtrl enemy)
        {
            Destroy(enemy.gameObject);
        }

    }
}
