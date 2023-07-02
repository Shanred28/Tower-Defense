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
        private static event Action<int> OnLifeUpdate;
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

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        [SerializeField] private Tower m_TowerPrefab;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCast);
            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite;
            tower.GetComponentInChildren<Turret>().turretProperties = towerAsset.turretProperties;
            tower.SetRadius(towerAsset.radius);
            Destroy(buildSite.gameObject);

        }
    }
}