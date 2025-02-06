using JetBrains.Annotations;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections; 

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject HealthBar;
    public GameObject CoinCount;

    //public bool fadein;
    //public bool fadeout;

    private void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        // StartCoroutine(LoadScene("MainMenu"));
    }

    public void LoadCredits()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Credits");
    }

    public void ResumePlay()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        HealthBar.SetActive(true);
        CoinCount.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }

       
       void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            HealthBar.SetActive(true);
            CoinCount.SetActive(true);
        }

        void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            HealthBar.SetActive(false);
            CoinCount.SetActive(false);
        }

        

    }
    //IEnumerator LoadScene(string sceneName)
   // {
        //fadein = true;
       // yield return new WaitForSeconds(1);
       // SceneManager.LoadScene(sceneName);
   // }
}
