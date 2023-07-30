using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image m_UpgradeIcon;
        [SerializeField] private Text m_LeveUp;
        [SerializeField] private Text m_CoastBuy;
        [SerializeField] private Button m_BuyButton;

        public void Initialize()
        {
            m_UpgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);
            m_LeveUp.text = $"Lvl: {savedLevel + 1}";
            m_CoastBuy.text = asset.coastByLevel[savedLevel].ToString();
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
        }
    }
}

