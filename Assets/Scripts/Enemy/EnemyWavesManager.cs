using System;
using UnityEngine;

namespace TowerDefence
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [SerializeField] private Path[] m_Paths;
        [SerializeField] private Enemy m_EnemyPrefabs;
        [SerializeField] private EnemyWave m_CurrentWave;

         private int m_ActiveEnemyCount = 0;

        public event Action OnAllWaveDead;
        private void RecordEnemyDead() 
        {
           if(--m_ActiveEnemyCount == 0)  
            {
                  ForceNextWave();
            }
        }


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
                        var e = Instantiate(m_EnemyPrefabs, m_Paths[pathIndex].startArea.GetRandominsideZone(), Quaternion.identity);
                        e.OnEnd += RecordEnemyDead;
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);
                        m_ActiveEnemyCount++;
                    }                   
                }
                else
                    Debug.Log($"Invalid pathIndex in {name}");
                
            }  
            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }

        public void ForceNextWave()
        {
            if (m_CurrentWave)
            {
                TDPlayer.Instance.ChangeGold((int)m_CurrentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {   
                if(m_ActiveEnemyCount == 0)
                     OnAllWaveDead?.Invoke();
            }
        }
    }
}

