using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Abilities : SingletonBase<Abilities>
    {

        [Serializable]
        public class FireAbility 
        {
            [SerializeField] private int m_Cost = 5;
            public int Cost => m_Cost;
            [SerializeField] private int m_Damage = 5;
            [SerializeField] private UpgradeAsset m_FireAbilityUpgrade;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private Text m_CostFireAbilityText;

            public void Initiate()
            {
                var levelFireAbility = Upgrades.GetUpgradeLevel(m_FireAbilityUpgrade);
                if (levelFireAbility > 0)
                {
                    Instance.m_FireButton.enabled = true;
                    m_Damage += m_Damage * levelFireAbility;
                    m_Cost += m_Cost / 2 * levelFireAbility;
                    Instance.m_FireButton.gameObject.SetActive(true);
                    m_CostFireAbilityText.text = m_Cost.ToString();
                }
                else 
                    Instance.m_FireButton.gameObject.SetActive(false);                
            }
            public void Use()
            {
                TDPlayer.Instance.ChangeMana(-m_Cost);
                ClickProtection.Instance.Activate((Vector2 v) =>
                 {
                     Vector3 position = v;
                     position.z = - Camera.main.transform.position.z;
                     position = Camera.main.ScreenToWorldPoint(position);
                     foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                     {
                         if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                         {
                             enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                         }
                     }
                 } );
            }
        }

        [Serializable]
        public class SlowAbility 
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Cooldawn = 15;
            public int Cost => m_Cost;
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private UpgradeAsset m_SlowAbilityUpgrade;
            [SerializeField] private Text m_CostSlowAbilityText;

            public void Initiate()
            {

                var levelSlowAbility = Upgrades.GetUpgradeLevel(m_SlowAbilityUpgrade);
                if (levelSlowAbility > 0)
                {
                    m_Cooldawn -= levelSlowAbility;
                    m_Duration += levelSlowAbility;
                    m_Cost += m_Cost /2  * levelSlowAbility;
                    Instance.m_TimeButton.gameObject.SetActive(true);
                    m_CostSlowAbilityText.text = m_Cost.ToString();
                }
                else
                    Instance.m_TimeButton.gameObject.SetActive(false);
            }
            public void Use()
            {
                TDPlayer.Instance.ChangeMana(-m_Cost);


                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }
             
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                }

                EnemyWavesManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }
                    EnemyWavesManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAgilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    Instance.isCooldawn = true;
                    Instance.m_TimeCooldawnText.gameObject.SetActive(true);
                    Instance.m_Timer = m_Cooldawn;
                    yield return new WaitForSeconds(m_Cooldawn);
                    Instance.m_Timer = 0;
                    Instance.m_TimeCooldawnText.gameObject.SetActive(false);
                    Instance.isCooldawn = false;
                    Instance.m_TimeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAgilityButton()); 
            }
        }
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private FireAbility m_FireAbility;
        
        [SerializeField] private Image m_TargetCircle;

        [SerializeField] private SlowAbility m_SlowAbility;
        [SerializeField] private Text m_TimeCooldawnText;

        private float m_Timer = 0;
        private bool isCooldawn;
        private void Start()
        {
            m_FireAbility.Initiate();
            m_SlowAbility.Initiate();
            m_TimeCooldawnText.gameObject.SetActive(false);
        }

        public void UseFireAbility()  => m_FireAbility.Use();
        public void UseSlowAbility()  => m_SlowAbility.Use();

        private void Update()
        {
            if (ManaStatusCheck(m_FireAbility.Cost))
                m_FireButton.interactable = true;
            else
                m_FireButton.interactable = false;

            if (ManaStatusCheck(m_SlowAbility.Cost) && !isCooldawn)
                m_TimeButton.interactable = true;
            else
                m_TimeButton.interactable = false;

            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
                m_TimeCooldawnText.text =  m_Timer.ToString();
            }        
        }
        private bool ManaStatusCheck(int costMana)
        {
            if (costMana <= TDPlayer.Instance.Mana)
            {
                return true;
            }
            else 
                return false;
        }
    }
}

