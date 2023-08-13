using System;
using UnityEngine;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        { get
            {
                return Player.Instance as TDPlayer;
            }
        }

        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_Gold);
        }
        public static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> action)
        {
            OnLifeUpdate += action;
            action(Instance.NumLives);
        }

        public static event Action<int> OnManaUpdate;
        public static void ManaUpdateSubscrible(Action<int> action)
        {
            OnManaUpdate += action;
            action((int)Instance.m_Mana);
        }


        [SerializeField] private Tower m_TowerPrefab;
        [SerializeField] private int m_Gold;
        [SerializeField] private float m_MaxMana;
        private float m_Mana = 0;
        public float Mana => m_Mana;
        [SerializeField] private float m_RegenMana;
        [SerializeField] private int m_ArmorDef;
        [SerializeField] private UpgradeAsset m_HealthUpgrade;
        [SerializeField] private UpgradeAsset m_GoldStarUpgrade;
        [SerializeField] private UpgradeAsset m_ArmorPlayer;
        [SerializeField] private UpgradeAsset m_MageTowerUpgrade;
        [SerializeField] private UpgradeAsset m_ArcherTowerUpgrade;
        [SerializeField] private UpgradeAsset m_IceTowerUpgrade;
        [SerializeField] private UpgradeAsset m_BalistaTowerUpgrade;

        private void Start()
        {
            var levelHealth = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            TakeDamage(-levelHealth * 5);
            var levelGoldStar = Upgrades.GetUpgradeLevel(m_GoldStarUpgrade);
            m_Gold += levelGoldStar * 5;
            var levelArmorPlayer = Upgrades.GetUpgradeLevel(m_ArmorPlayer);
            m_ArmorDef += levelArmorPlayer;
            m_Mana = m_MaxMana;

        }

        public static void GoldUpdateSubscribeRemove(Action<int> action)
        {

            OnGoldUpdate -= action;
            action(Instance.m_Gold);
        }
        public static void LifeUpdateSubscribeRemove(Action<int> action)
        {
            OnLifeUpdate -= action;
            action(Instance.NumLives);
        }
        public static void ManaUpdateSubscribleRemove(Action<int> action)
        {
            OnManaUpdate -= action;
            action((int)Instance.m_Mana);
        }


        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldUpdate(m_Gold);
        }

        public void ReduceLife(int change)
        {
            if (m_ArmorDef < change)
            {
                TakeDamage(change - m_ArmorDef);
                OnLifeUpdate(NumLives);
            }
        }

        public void ChangeMana(int change)
        { 
           m_Mana += change;
            OnManaUpdate((int)m_Mana);
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCast);
            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            tower.SetRadius(towerAsset.radius);
            buildSite.gameObject.SetActive(false);

        }

        private void Update()
        {
            if (m_Mana <= m_MaxMana)
            {
                m_Mana += m_RegenMana * Time.deltaTime;
                OnManaUpdate((int)m_Mana);
            }
        }
    }
}