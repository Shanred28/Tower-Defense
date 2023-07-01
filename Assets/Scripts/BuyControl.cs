using UnityEngine;

namespace TowerDefence
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_RTransform;
        private void Awake()
        {
            m_RTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToTransform;
            gameObject.SetActive(false);
        }

        private void MoveToTransform(Transform target)
        {
            if (target)
            {
                var pos = Camera.main.WorldToScreenPoint(target.position);
                gameObject.SetActive(true);
                m_RTransform.anchoredPosition = pos;
            }
            else 
                gameObject.SetActive(false);
            
        }
    }
}

