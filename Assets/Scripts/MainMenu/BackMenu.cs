using UnityEngine;

namespace TowerDefence
{
    public class BackMenu : MonoBehaviour
    {
        public void ReturnMenu()
        { 
          LevelSequenceController.Instance.ReturMainMenu();
        }

        public void ReturnMap()
        {
            LevelSequenceController.Instance.ReturnMapLevel();
        }
    }
}

