using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        [SerializeField] private RectTransform m_ReesultPanel;
        [SerializeField] private Image[] m_ResultImages;
        [SerializeField] private GameObject m_IconActive;
        [SerializeField] private GameObject m_IconComplete;

        public bool IsComplete { get { return gameObject.activeSelf && 
                    m_ReesultPanel.gameObject.activeSelf; } }

        public void LoadLevel()
        {
            Debug.Log(m_Episode);
               LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
           

        }

        public void Initialise()
        {
            var score= MapCompletion.Instance.GetEposideScore(m_Episode);
            print(m_Episode);
            if (score > 0)
            {
                m_ReesultPanel.gameObject.SetActive(true);
                m_IconActive.gameObject.SetActive(false);
            }

            for (int i = 0; i < score; ++i)
            {
                m_ResultImages[i].color = Color.white;
            }
        }
    }
}

