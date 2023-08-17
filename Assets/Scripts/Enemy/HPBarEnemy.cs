using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class HPBarEnemy : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Enemy;
        [SerializeField] private Image m_HPBar;


        private void Update()
        {
            m_HPBar.fillAmount = (float) (m_Enemy.CurrentHitPoints / m_Enemy.HitPoints);
        }


    }
}

