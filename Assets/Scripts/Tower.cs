using UnityEditor;
using UnityEngine;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        [SerializeField] private float m_Lead;
        private Turret[] turrets;
        private Enemy target = null;

        public void Use(TowerAsset asset)
        {
            turrets = GetComponentsInChildren<Turret>();
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;
            foreach (Turret t in turrets) 
            {
                t.AssignLoadout(asset.turretProperties);
            }
           GetComponentInChildren<BuildSite>().SetBuildableTowers(asset.m_UpgradesTo);
        }

        private void Update()
        {
            if (target)
            {
                if (Vector3.Distance(target.transform.position, transform.position)  <= m_Radius)
                {
                    foreach (Turret turret in turrets)
                    {
                        turret.transform.up = target.transform.position - turret.transform.position + (Vector3)target.GetComponent<Rigidbody2D>().velocity * m_Lead;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }              
            }

            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Enemy>();
                }
            }         
        }

        public void SetRadius(float radius)
        {
            m_Radius = radius;
        }

       private static Color GismaColor = new Color(300, 0, 0, 0.3f);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
   {
       Handles.color = GismaColor;
       Handles.DrawWireDisc (transform.position, transform.forward, m_Radius);
   }
#endif
}
}
