using UnityEngine;

namespace TowerDefence
{
    public class TDProjectile : Projectile
    {
        public enum DamageType
        { 
            Base,
            Magic
        }

        [SerializeField] private DamageType m_DamageType;
        [SerializeField] private Sound m_ShotSound = Sound.Arrow;

        private void Start()
        {
            m_ShotSound.Play();
        }

        protected override void OnHit(RaycastHit2D hit)
        {
            Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);
            }
        }
    }
}

