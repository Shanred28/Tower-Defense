using System;
using UnityEngine;

namespace TowerDefence
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string filename = "completion.dat";
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

/*        public static void ResetSavedData()
        {
            Saver<EpisodeScore[]>.Reset(filename);
        }*/
        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            else
                Debug.Log($"Epsidoe complete with score {levelScore}");
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_CompletionsData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, m_CompletionsData);
                    }                      
                }
            }
        }

        [SerializeField] private EpisodeScore[] m_CompletionsData;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref m_CompletionsData);
        }

        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_CompletionsData.Length)
            {
                episode = m_CompletionsData[id].episode;
                score = m_CompletionsData[id].score;
                return true;
            }

            episode = null;
            score = 0;
            return false;
        }
        

    }
}

