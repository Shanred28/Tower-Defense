using UnityEngine;

namespace TowerDefence
{

    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("VisualModel")]
        public  Color color = Color.white;
        public   Vector2 spriteScale = new Vector2(3,3);
        public RuntimeAnimatorController animations;
        [Header("Characteristics")]
        public float moveSpeed = 1f;
        public int hp = 1;
        public int score = 1;
        public float radius = 0.26f;
    }

}

