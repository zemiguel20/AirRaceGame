using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}