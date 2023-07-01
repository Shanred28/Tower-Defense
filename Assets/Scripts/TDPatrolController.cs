using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public class TDPatrolController : AiController
    {
        private Path m_Path;
        private int m_PathIndex;
        [SerializeField] private UnityEvent OnEndPath;
        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            m_PathIndex = 0;
            SetPatrolBehaviour(newPath[m_PathIndex]);
        }

        protected override void GetNewPoint()
        {
            if (m_Path.Lenght > ++m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}

