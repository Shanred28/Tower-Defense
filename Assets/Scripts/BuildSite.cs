using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<Transform> OnClickEvent;
        public static void HideBuyControl()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }
    }
}