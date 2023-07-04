using UnityEngine;

namespace TowerDefence
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] m_Levels;

        private void Start()
        {
            var drawLevel = 0;
            var score = 1;

            while (score != 0 && drawLevel < m_Levels.Length && MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                m_Levels[drawLevel].SetLevelData(episode, score);
                drawLevel += 1;
                if (score == 0) break;
            }

            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                m_Levels[i].gameObject.SetActive(false);
            }
        }
    }
}

