using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Abilities : SingletonBase<Abilities>
    {
        public interface Usable { void Use(); }

        [Serializable]
        public class FireAbility : Usable
        {
            [SerializeField] private int m_Cost = 5;
            [SerializeField] private int m_Damage = 2;
            public void Use()
            {
                
            }
        }

        [Serializable]
        public class SlowAbility : Usable
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Cooldawn = 15;
            [SerializeField] private float m_Duration = 5;
            public void Use()
            {
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

                IEnumerator TimeAnilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldawn);
                    Instance.m_TimeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAnilityButton()); 
            }
        }
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private FireAbility m_FireAbility;
        [SerializeField] private SlowAbility m_SlowAbility;
        public void UseFireAbility()  => m_FireAbility.Use();
        public void UseSlowAbility()  => m_SlowAbility.Use();

    }
}

