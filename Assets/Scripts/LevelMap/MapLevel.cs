using UnityEngine;

namespace TowerDefence
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        public void LoadLevel()
        {
               LevelSequenceController.Instance.StartEpisode(m_Episode);
        }
    }
}

