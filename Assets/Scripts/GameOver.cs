using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(4);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
