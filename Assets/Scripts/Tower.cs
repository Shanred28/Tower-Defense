using UnityEditor;
using UnityEngine;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] turrets;
        private Destructible target;

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (Turret turret in turrets)
                    {
                        turret.transform.up = targetVector;
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
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }         
        }

        private static Color GismaColor = new Color(300, 0, 0, 0.3f);
#if     UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GismaColor;
            Handles.DrawWireDisc (transform.position, transform.forward, m_Radius);
        }
#endif
    }
}
