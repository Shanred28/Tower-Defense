using System;
using UnityEditor;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(TDPatrolController))]
    [RequireComponent(typeof(Destructible))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage;
        [SerializeField] private int m_Gold;
        [SerializeField] private int m_Armor;

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
            m_Armor = asset.armor;
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

        public void TakeDamage(int damage)
        {
            m_Destructible.ApplyDamage(Mathf.Max(1,damage-m_Armor));
        }

    }
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

}
