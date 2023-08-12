using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<BuildSite> OnClickEvent;
        public TowerAsset[] buildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers) 
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);              
            }
            else
                buildableTowers = towers;
        } 
        public static void HideBuyControl()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }
}