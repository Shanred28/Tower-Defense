using System;
using UnityEngine;

namespace TowerDefence
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }

        public const string filename = "upgrades.dat";


        [SerializeField] private UpgradeSave[] save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref save);
        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.save);
                }
            }
        }
        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
        }

        public static int GetTotalCoast()
        {
            int result = 0;
            foreach (var upgrade in Instance.save) 
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.coastByLevel[i];
                }
            }
            return result;
        }
    }
}

