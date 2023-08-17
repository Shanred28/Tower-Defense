using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip m_BGM;
        private AudioSource m_AudioSource;

        private new void Awake()
        {
            base.Awake();
            m_AudioSource = GetComponent<AudioSource>();
            Instance.m_AudioSource.clip = m_BGM;
            Instance.m_AudioSource.Play();
            Instance.m_AudioSource.volume = 0.05f;
        }

        public void Play(Sound sound)
        {
            m_AudioSource.PlayOneShot(m_Sounds[sound]);
        }
    }
}

