using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private Image m_UpgradeIcon;
        [SerializeField] private Text m_LeveUp;
        [SerializeField] private Text m_CoastBuy;
        [SerializeField] private Button m_BuyButton;

        public void SetUpgrade(UpgradeAsset asset, int level = 1)
        {
            m_UpgradeIcon.sprite = asset.sprite;
            m_LeveUp.text = level.ToString();
            m_CoastBuy.text = asset.coastByLevel[level].ToString();
        }
    }
}

