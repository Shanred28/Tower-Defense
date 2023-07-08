using UnityEngine;

namespace TowerDefence
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;
        public bool IsCompleted { get { return isCompleted; } }

        private void Start()
        {
            FindObjectOfType<EnemyWavesManager>().OnAllWaveDead += () =>
            {
                isCompleted = true;
            };
        }
    }
}

