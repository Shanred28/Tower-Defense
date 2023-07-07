using UnityEngine;

namespace TowerDefence
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [SerializeField] private Path[] m_Paths;
        [SerializeField] private Enemy m_EnemyPrefabs;
        [SerializeField] private EnemyWave m_CurrentWave;

        private void Start () 
        {
            m_CurrentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in m_CurrentWave.EnumeratorSquads())
            {
                if (pathIndex < m_Paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefabs);
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);
                    }                   
                }
                else
                    Debug.Log($"Invalid pathIndex in {name}");
                
            }  
            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }
    }
}

