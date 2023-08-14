using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu]
    public class Sounds : ScriptableObject
    {
        public AudioClip[] m_Sounds;
        public AudioClip this[Sound sound] => m_Sounds[(int) sound];
    }
}
