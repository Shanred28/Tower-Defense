using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class MapLevel : MonoBehaviour
    {
        private Episode m_Episode;
        [SerializeField] private Text m_Text;
        public void LoadLevel()
        {
               LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            m_Episode = episode;
            m_Text.text = $"{score}/3";
        }
    }
}

