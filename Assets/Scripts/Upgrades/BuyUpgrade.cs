using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image m_UpgradeIcon;
        private int m_CoastNumber = 0;
        [SerializeField] private Text m_LeveUpText;
        [SerializeField] private Text m_CoastText;
        [SerializeField] private Button m_BuyButton;

        public void Initialize()
        {
            m_UpgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);
            
            if (savedLevel >= asset.coastByLevel.Length)
            {
                m_LeveUpText.text = $"Lvl: {savedLevel}" + "(Max)";
                m_BuyButton.interactable = false;
                m_BuyButton.transform.Find("Image_Star").gameObject.SetActive(false);
                m_BuyButton.transform.Find("Text_Title").gameObject.SetActive(false);
                m_CoastText.text = "X";
                m_CoastNumber = int.MaxValue;

            }
            else
            {                
                m_LeveUpText.text = $"Lvl: {savedLevel + 1}";
                m_CoastNumber = asset.coastByLevel[savedLevel];
                m_CoastText.text = m_CoastNumber.ToString();
            }                
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

        internal void CheckCost(int money)
        {
            m_BuyButton.interactable = money >= m_CoastNumber;
        }
    }
}

