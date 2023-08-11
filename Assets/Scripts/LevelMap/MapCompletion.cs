using System;
using UnityEngine;

namespace TowerDefence
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public const string filename = "completion.dat";
        
        [SerializeField] private EpisodeScore[] m_CompletionsData;
        public int TotalScore { get { return m_TotalScore; } }

        private int m_TotalScore;

        private new void Awake()
        {
            base.Awake();

            Saver<EpisodeScore[]>.TryLoad(filename, ref m_CompletionsData);

            foreach (var episodeScore in m_CompletionsData)
            {
                m_TotalScore += episodeScore.score;
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }

            else
                Debug.Log($"Epsidoe complete with score {levelScore}");
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {

            foreach (EpisodeScore item in m_CompletionsData)
            {
                if (item.episode == currentEpisode)
                {
                    Debug.Log("SaveResult");
                    if (levelScore > item.score)
                    {
                        m_TotalScore += levelScore - item.score;
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, m_CompletionsData);
                    }
                }
            }
        }

        public int GetEposideScore(Episode m_Episode)
        {
            foreach (EpisodeScore data in m_CompletionsData)
            {
                if (data.episode == m_Episode)
                {
                    return data.score;
                }
            }
            return 0;

        }
    }
}

