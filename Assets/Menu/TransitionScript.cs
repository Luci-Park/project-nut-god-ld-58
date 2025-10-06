using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    private string sceneName = "NutopiaGameScene";

    public void Trigger()
    {
        SceneManager.LoadScene(sceneName);
    }
}
