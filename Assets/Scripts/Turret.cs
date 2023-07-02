using UnityEngine;

namespace TowerDefence
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [HideInInspector] public TurretProperties turretProperties;

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
            if(turretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_Ship)
            {
                if (m_Ship.DrawEnergy(turretProperties.EnergyUsage) == false) return;

                if (m_Ship.DrawAmmo(turretProperties.AmmoUsage) == false) return;
            }
           

            Projectile projectile = Instantiate(turretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

           // projectile.SetPerentShooter(m_Ship);

            m_RefireTimer = turretProperties.RateOfFire;

            m_AudioSource.PlayOneShot(turretProperties.LaunchSFX);
            
        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            turretProperties = props;
        }

        #endregion
    }
}

