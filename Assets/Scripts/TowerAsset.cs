using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu]
    public class TowerAsset: ScriptableObject
    {
        public int goldCast = 15;
        public Sprite GUISprite;
        public Sprite sprite;
        public TurretProperties turretProperties;
        public float radius = 3.0f;

    }
   
}

