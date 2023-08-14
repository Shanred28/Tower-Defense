using UnityEngine;

namespace TowerDefence
{
    public class SoundHook : MonoBehaviour
    {
        [SerializeField] private Sound m_Sound;

        public void Play() { m_Sound.Play(); }
    }
}

