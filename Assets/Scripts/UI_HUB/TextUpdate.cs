using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource {Gold, Life, Mana }
        public UpdateSource  source = UpdateSource.Gold;
        private Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();
            switch (source)
            {
                case UpdateSource.Gold: 
                    TDPlayer.GoldUpdateSubscribe(UpdateText);
                    break;

                case UpdateSource.Life:
                    TDPlayer.LifeUpdateSubscribe(UpdateText);
                    break;

                case UpdateSource.Mana:
                TDPlayer.ManaUpdateSubscrible(UpdateText);
                break;
            }
        }
        private void UpdateText(int num)
        {
            m_Text.text = num.ToString();
        }

        private void OnDestroy()
        {
            switch (source)
            {
                case UpdateSource.Gold:
                    TDPlayer.GoldUpdateSubscribeRemove(UpdateText);
                    break;

                case UpdateSource.Life:
                    TDPlayer.LifeUpdateSubscribeRemove(UpdateText);
                    break;
                    
                case UpdateSource.Mana:
                    TDPlayer.ManaUpdateSubscribleRemove(UpdateText);
                    break;
            }
        }
    }
}

