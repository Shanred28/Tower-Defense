using System.Collections;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region Properties
        /// <summary>
        /// Weight to indicate in Rigidbody.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Force pushing forward.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotation force.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Maximum linear speed.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        private float m_MaxLinearVelocityBeckup;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        public void HalfMaxLinearVelocity() { m_MaxLinearVelocityBeckup = m_MaxLinearVelocity; m_MaxLinearVelocity /= 2; }
        public void RestoreMaxLinearVelocity() { m_MaxLinearVelocity = m_MaxLinearVelocityBeckup; }

        /// <summary>
        /// Maximum rotation speed. In Degrees/Seconds.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        /// <summary>
        /// Reference to Rigidbody2D.
        /// </summary>
        private Rigidbody2D m_Rigid;

        #endregion

        #region Public API
        /// <summary>
        /// Linear thrust control. From -1.0 to +1.0.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotational thrust control. From -1.0 to +1.0.
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1;
        }

        private void FixedUpdate()
        {
               UpdateRigiBody();
        }
        #endregion

        /// <summary>
        /// Method of adding forces to control the ship.
        /// </summary>
        private void UpdateRigiBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            return;
            /*for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }           
            }*/
        }

        [SerializeField] private int m_MaxEnergy;
        public int MaxEnergy => m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_CurrentEnergy;
        public float CurrentEnergy => m_CurrentEnergy;
        private int m_CurrentAmmo;
        public int CurrentAmmo => m_CurrentAmmo;


        public bool DrawEnergy(int count)
        {
            return true;
        }

        public bool DrawAmmo(int count)
        {
            return true;
        }


        public new void Use(EnemyAsset asset)
        {
            m_MaxLinearVelocity = asset.moveSpeed;
            base.Use(asset);
        }

        private bool freeze = false;
        public void FreezeMove(float timer)
        {
           
            if (!freeze)
            {
                if (m_Rigid != null)
                {
                    m_Rigid.constraints = RigidbodyConstraints2D.FreezePosition;
                    freeze = true;
                    StartCoroutine(WaitTimerThrust(timer));
                }            
            }
            
        }

        IEnumerator WaitTimerThrust(float timer)
        {
            
            yield return new WaitForSeconds(timer);
            m_Rigid.constraints = RigidbodyConstraints2D.None;
            freeze = false;
          
        }
    }
}

