using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MainMenu()
    {
        SceneManager.LoadScene(4);
    }
}
