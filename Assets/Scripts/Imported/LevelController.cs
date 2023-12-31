using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }

    }

    public class LevelController : SingletonBase<LevelController>
    {

        [SerializeField] protected float m_ReferenceTime;
        public float RefereenceTime => m_ReferenceTime;

        [SerializeField] protected UnityEvent m_EventLevelCompleted;
        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        [SerializeField] private float m_TimeBonusScore;
        public float TimeBonusScore => m_TimeBonusScore;

        protected void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        { 
            if(m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;

            foreach (var v in m_Conditions)
            {
                if (v.IsCompleted)
                    ++numCompleted;
            }

            if (numCompleted == m_Conditions.Length)
            { 
                m_IsLevelCompleted = true;
                m_EventLevelCompleted?.Invoke();
                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }
    }
}
