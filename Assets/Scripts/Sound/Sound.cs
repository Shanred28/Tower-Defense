namespace TowerDefence
{
    public enum Sound 
    {
       BGM,
       Arrow,
       ArrowHit
    }
    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        { 
             SoundPlayer.Instance.Play(sound);
        }
    }
}

