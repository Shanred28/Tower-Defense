using UnityEngine.UI;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel m_RootLevel;
        [SerializeField] private int m_NeedPoints = 3;
        [SerializeField] private Text m_TextNeedPoint;


        internal void TryActivate()
        {
            gameObject.SetActive(m_RootLevel.IsComplete);

            if (m_NeedPoints > MapCompletion.Instance.TotalScore)
            {
                m_TextNeedPoint.text = m_NeedPoints.ToString();
              
            }
            else
            { 
                m_TextNeedPoint.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }           
        }
    }        
}

