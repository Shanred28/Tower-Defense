using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu]
    public class UpgradeAsset : ScriptableObject
    {
        public Sprite sprite;

        public int[] coastByLevel = { 3 };
    }
}

