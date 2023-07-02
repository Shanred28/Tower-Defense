using UnityEngine;

namespace TowerDefence
{

    public class WaveControl : MonoBehaviour
    {
        public enum ContolEnemySpawner
        {
            AddNumSpawn,
            EnableSpawn,
            SwithPatch
        }

        [SerializeField] private EnemySpawner enemySpawner;

        [SerializeField] private ContolEnemySpawner m_ModeSpawner;

        [SerializeField] private Path[] m_Paths;

        [SerializeField] private float timer;
        private float time;

        private void Start () 
        {
            time = timer;
        }

        private void Update () 
        {
            if(time > 0 )
                time -= Time.deltaTime;

            if (time <= 0)
            {
                time = timer;
                if(m_ModeSpawner == ContolEnemySpawner.AddNumSpawn)
                    AddWave();
                if (m_ModeSpawner == ContolEnemySpawner.EnableSpawn)
                    ActiveSpawner();
                if (m_ModeSpawner == ContolEnemySpawner.SwithPatch)
                    SeleectPath();


            }
        }

        private void AddWave()
        {
            enemySpawner.AddNumSpawn();
        }

        private void ActiveSpawner()
        {
            enemySpawner.enabled = true;
        }

        private void SeleectPath()
        {
            enemySpawner.m_Path = m_Paths[Random.Range(0, m_Paths.Length)];
        }
    }
}

