using UnityEngine;

namespace TowerDefence
{
    public class Projectile : Entity
    {
        [SerializeField] protected float m_Velocity;
        public float VelocityProjectile => m_Velocity;

        [SerializeField] protected float m_LifeTime;

        [SerializeField] protected int m_Damage;

        [SerializeField] protected ImpactEffect m_ImpactEffectPrefab;

        [SerializeField] private bool effectProjectile = false;

        protected float m_Timer;

        [SerializeField] private bool isNotDestroy = false;

        protected virtual void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            
            if (hit)
            {
                OnHit(hit);

                if (!isNotDestroy)
                    OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        protected virtual void OnHit(RaycastHit2D hit)
        {

        }

        protected void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        { 
            Destroy(gameObject);
        }
    }
}

