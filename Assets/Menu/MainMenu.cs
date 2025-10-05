using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void GameCredits()
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
