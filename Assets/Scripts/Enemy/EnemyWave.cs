using System;
using UnityEngine;
using System.Collections.Generic;

namespace TowerDefence
{
    public class EnemyWave : MonoBehaviour
    {
        public static Action<float> OnWavePrepare;

        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;

        }

        [Serializable]
        private class PatchGroup
        {
            public Squad[] suads;
        }

        [SerializeField] private PatchGroup[] groups;


        [SerializeField] private float m_PrepareTime = 10f;
        public float GetRemainingTime() { return m_PrepareTime - Time.time; }

        private void Awake()
        { 
           enabled = false;
        }

        private void Update()
        {
            if (Time.time >= m_PrepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumeratorSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].suads)
                {
                    yield return (squad.asset, squad.count, i);
                }                
            }         
        }

        private event Action OnWaveReady;
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_PrepareTime);
            enabled = true;
            m_PrepareTime += Time.time;
            OnWaveReady += spawnEnemies;
           
        }
        [SerializeField] private EnemyWave nextWave;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if(nextWave)nextWave.Prepare(spawnEnemies);
            return nextWave;
        }
    }
}