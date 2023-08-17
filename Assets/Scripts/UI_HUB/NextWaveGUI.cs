using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class NextWaveGUI : MonoBehaviour
    {
        private EnemyWavesManager m_Manager;

        [SerializeField] private Text m_BonusAmount;
        [SerializeField] private int m_BonusGold = 2;
        private float m_TimeToNextWave;

        private void Start()
        {
            m_Manager = FindObjectOfType<EnemyWavesManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                m_TimeToNextWave = time;
            };
        }
        private void Update() 
        {
            var bonus = (int)m_TimeToNextWave * m_BonusGold;
            if (bonus < 0) bonus = 0;
            m_BonusAmount.text = bonus.ToString();
            m_TimeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }
    }
}

