using UnityEngine;

namespace TowerDefence
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        [SerializeField] private AudioSource m_AudioSource;

        #region Unity Event 
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if(m_RefireTimer > 0)
                 m_RefireTimer -= Time.deltaTime;

            else if(Mode == TurretMode.Auto)
                Fire();
        }
        #endregion


        #region Public API

        public void Fire()
        { 
            if(m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_Ship)
            {
                if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;

                if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            }
           

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

           // projectile.SetPerentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            m_AudioSource.PlayOneShot(m_TurretProperties.LaunchSFX);
            
        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }

        #endregion
    }
}

