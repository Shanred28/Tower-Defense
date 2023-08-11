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

        [SerializeField] private int m_Gold;

        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldUpdate(m_Gold);
        }

        [SerializeField] private int m_ArmorDef;
        public void ReduceLife(int change)
        {
            if (m_ArmorDef < change)
            {
                TakeDamage(change - m_ArmorDef);
                OnLifeUpdate(NumLives);
            }

        }

        [SerializeField] private Tower m_TowerPrefab;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCast);
            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            tower.SetRadius(towerAsset.radius);
            buildSite.gameObject.SetActive(false);

        }
        [SerializeField] private UpgradeAsset m_HealthUpgrade;
        [SerializeField] private UpgradeAsset m_GoldStarUpgrade;
        [SerializeField] private UpgradeAsset m_ArmorPlayer;
        private new void Awake()
        {
            base.Awake();

            var levelHealth = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            TakeDamage(-levelHealth * 5);
            var levelGoldStar = Upgrades.GetUpgradeLevel(m_GoldStarUpgrade);
            m_Gold += levelGoldStar * 5;
            var levelArmorPlayer = Upgrades.GetUpgradeLevel(m_ArmorPlayer);
            m_ArmorDef += levelArmorPlayer;
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

    }
}