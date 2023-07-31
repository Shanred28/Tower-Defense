using UnityEngine;

namespace TowerDefence
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_RTransform;
        private void Awake()
        {
            m_RTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var pos = Camera.main.WorldToScreenPoint(buildSite.position);
                Debug.Log(gameObject);
                gameObject.SetActive(true);
                m_RTransform.anchoredPosition = pos;
            }
            else 
                gameObject.SetActive(false);

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
    }
}

