using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    /// <summary>
    /// Панель результатов уровня. Должна лежать в каждом уровне без галочки DoNotDestroyOnLoad.
    /// </summary>
    public class LevelResultController : SingletonBase<LevelResultController>
    { 

        [SerializeField] private GameObject m_PanelSuccess;
        [SerializeField] private GameObject m_PanelFailure;

        [SerializeField] private Text m_LevelTime;

        [SerializeField] private Image m_StarsTwo;
        [SerializeField] private Image m_StarsTree;

        private int m_Scorelevel;

        /// <summary>
        /// Показываем окошко результатов. Выставляем нужные кнопочки в зависимости от успеха.
        /// </summary>
        /// <param name="result"></param>
        public void Show(bool result)
        {
            m_PanelSuccess?.gameObject.SetActive(result);
            if (result)
            {
                switch (m_Scorelevel) 
                { 
                    case 2:
                        m_StarsTwo.gameObject.SetActive(true);
                        break;
                    case 3:
                        m_StarsTwo.gameObject.SetActive(true);
                        m_StarsTree.gameObject.SetActive(true);
                        break;
                }
            }
            m_PanelFailure?.gameObject.SetActive(!result);
        }

        /// <summary>
        /// Запускаем следующий уровен. Дергается эвентом с кнопки play next.
        /// </summary>
        public void OnPlayNext()
        {
            LevelSequenceController.Instance.AdvanceLevel();
        }

        /// <summary>
        /// Рестарт уровня. Дергается эвентом с кнопки restart в случае фейла уровня.
        /// </summary>
        public void OnRestartLevel()
        {
            LevelSequenceController.Instance.RestartLevel();
        }

        public void ReturnMap()
        {
            LevelSequenceController.Instance.ReturnMapLevel();
        }

        public class Stats
        {
            public int numKills;
            public float time;
            public int score;
        }

        /// <summary>
        /// Общая статистика за эпизод.
        /// </summary>
        public static Stats TotalStats { get; private set; }

        /// <summary>
        /// Сброс общей статистики за эпизод. Вызывается перед началом эпизода.
        /// </summary>
        public static void ResetPlayerStats()
        {
            TotalStats = new Stats();
        }

        /// <summary>
        /// Собирает статистику по текущему уровню.
        /// </summary>
        /// <returns></returns>
        private void UpdateCurrentLevelStats()
        {
            TotalStats.numKills += Player.Instance.NumKills;
            TotalStats.time += LevelController.Instance.LevelTime;
            TotalStats.score += Player.Instance.Score;

            // бонус за время прохождения.
            int timeBonus = (int) (LevelController.Instance.RefereenceTime - (int)LevelController.Instance.LevelTime);

            if (timeBonus > 0)
                TotalStats.score += timeBonus;
        }
        public int ScoreLevel(int score)
        {
           return m_Scorelevel = score;
        }
    }
}