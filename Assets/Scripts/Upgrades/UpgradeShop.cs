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
            foreach (var slot in m_buyUpgradesSeles)
            {
                slot.Initialize();
                slot.transform.Find("Button_Buy").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();
        }
        public void UpdateMoney() 
        {
            m_Money = MapCompletion.Instance.TotalScore;
            m_Money -= Upgrades.GetTotalCoast();
            _m_MoneyText.text = m_Money.ToString();
            foreach (var slot in m_buyUpgradesSeles)
            {
                slot.CheckCost(m_Money);
            }
        }
    }
}

