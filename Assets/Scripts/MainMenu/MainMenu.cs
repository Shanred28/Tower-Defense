using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button m_Continue;
        private void Start()
        {
            m_Continue.interactable = FileHandler.HaveFile(MapCompletion.filename);
        }

        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.filename);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        { 
            Application.Quit();
        }

    }
}

