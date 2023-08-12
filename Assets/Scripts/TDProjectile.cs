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
