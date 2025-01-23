using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevel01()
    {
        SceneManager.LoadScene("Level01-Test");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToHelp()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            SceneManager.LoadScene("Level02-Test");
        }
    }
}
