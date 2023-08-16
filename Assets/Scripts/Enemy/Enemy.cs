using System;
using UnityEngine;
using static TowerDefence.TDProjectile;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefence
{
    [RequireComponent(typeof(TDPatrolController))]
    [RequireComponent(typeof(Destructible))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType { Base = 0, Mage = 1 }
        private static Func<int, DamageType, int, int>[] ArmorDamageFunktions =
        {  //ArmorType.Base
           (int power, DamageType type, int armor) =>
           {
               switch(type)
               {
                case DamageType.Magic: return power;
                       default: return Mathf.Max(power - armor,1);
               }
           },

           (int power, DamageType type, int armor) =>
           {   //ArmorType.Magic
               if(DamageType.Base == type)
               {
                  armor = armor / 2;
               }
               return Mathf.Max(power - armor,1);
}          
        };

        [SerializeField] private int m_Damage;
        [SerializeField] private int m_Gold;
        [SerializeField] private int m_Armor;
        [SerializeField] private ArmorType m_ArmorType;

        private Destructible m_Destructible;

        public event Action OnEnd;

        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();
        }

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            var col = GetComponentInChildren<CircleCollider2D>();
            col.radius = asset.radius;

            m_Damage = asset.damage;
            m_Armor= asset.armor;
            m_ArmorType = asset.armorType;
            m_Gold = asset.gold;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }

        public void TakeDamage(int damage, DamageType damageType)
        {
            m_Destructible.ApplyDamage(ArmorDamageFunktions[(int)m_ArmorType](damage,damageType, m_Armor));
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            { 
                (target as Enemy).Use(a);
            }
        }
    }
#endif
}
