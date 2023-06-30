using UnityEngine;

namespace TowerDefence
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        { 
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefab;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private SpawnMode m_SpawnMode;
        [SerializeField] private int m_NumSpawns;
        [SerializeField] private float m_RespawnTime;
        [SerializeField] private Path m_Path;

        private float m_Timer;

        private void Start () 
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update() 
        { 
            if(m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_Timer < 0 && m_SpawnMode == SpawnMode.Loop)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_EntityPrefab.Length);

                GameObject e = Instantiate(m_EntityPrefab[index].gameObject);

                e.transform.position = m_Area.GetRandominsideZone();
                if(e.TryGetComponent<TDPatrolController>(out var ai))
                    ai.SetPath(m_Path);
            }
        }
    }
}