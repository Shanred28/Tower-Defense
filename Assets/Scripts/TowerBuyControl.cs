using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public partial class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_Tower;

        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_ButtonBuy;

        [SerializeField] private Transform buildSite;
        public void SetBuildSite(Transform value)
        {
            buildSite = value;
        }

        private void Start()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_Tower.goldCast.ToString();
            m_ButtonBuy.GetComponent<Image>().sprite = m_Tower.GUISprite;
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_Tower.goldCast != m_ButtonBuy.interactable)
            {
                m_ButtonBuy.interactable = !m_ButtonBuy.interactable;
                m_Text.color = m_ButtonBuy.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_Tower, buildSite);
            BuildSite.HideBuyControl();
        }

        private void OnDestroy()
        {
            TDPlayer.GoldUpdateSubscribeRemove(GoldStatusCheck);
        }
    }
}

