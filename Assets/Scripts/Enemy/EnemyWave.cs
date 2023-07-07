using System;
using UnityEngine;
using System.Collections.Generic;

namespace TowerDefence
{
    public class EnemyWave : MonoBehaviour
    {
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
            yield return (groups[0].suads[0].asset, groups[0].suads[0].count, 0);
        }

        private event Action OnWaveReady;
        public void Prepare(Action spawnEnemies)
        {
            enabled = true;
            m_PrepareTime += Time.time;
            OnWaveReady += spawnEnemies;
        }

        internal EnemyWave PrepareNext(Action spawnEnemies)
        {
            return null;
        }
    }
}