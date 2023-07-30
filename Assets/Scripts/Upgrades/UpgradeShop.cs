using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UpgradeShop : MonoBehaviour
    {

        [SerializeField] private int m_Money;
        [SerializeField] private Text _m_MoneyText;
        [SerializeField] private BuyUpgrade[] m_buyUpgradesSeles;

        private void Start()
        {
            m_Money = MapCompletion.Instance.TotalScore;
            _m_MoneyText.text = m_Money.ToString();
            foreach (var slot in m_buyUpgradesSeles)
            {
                slot.Initialize();
            }
        }
    }
}

