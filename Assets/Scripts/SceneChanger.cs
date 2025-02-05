using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public bool fadein;
    public bool fadeout;
    public GameObject PausemenuUI;

    public float timeToFade;

    public AudioSource music;

    void Start()
    {
        fadeout = true;
        Debug.Log("went through");
    }
    void Update()
    {
        Debug.Log(fadeout);
        if(fadein == true)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }

        if(fadeout == true)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                while(fadeout == true)
                {
                    Debug.Log("fading");
                    music.volume += 0.01f;
                }
                if(canvasGroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }
    public void GoToMainMenu()
    {
        StartCoroutine(LoadScene("MainMenu"));
    
    }

    public void GoToLevel01()
    {
        StartCoroutine(LoadScene("Level01-Test"));
    }

    public void GoToCredits()
    {
        StartCoroutine(LoadScene("Credits"));
       
    }

    public void GoToHelp()
    {
        StartCoroutine(LoadScene("HelpMenu"));
       
    }

    public void Quit()
    {
        Application.Quit();
        
    }

    public void Resume()
    {
        PausemenuUI.SetActive(false);
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit" && SceneManager.GetActiveScene().name == "Level01-Test" && GameObject.FindGameObjectsWithTag("coin") == null)
        {
            StartCoroutine(LoadScene("Level02-Test"));
        }
        else if(other.tag == "Exit" && SceneManager.GetActiveScene().name == "Level02-Test" && GameObject.FindGameObjectsWithTag("coin") == null)
        {
            StartCoroutine(LoadScene("Level03-Test"));
           
        }
        else if(other.tag == "Exit" && SceneManager.GetActiveScene().name == "Level03-Test" && GameObject.FindGameObjectsWithTag("coin") == null)
        {
            StartCoroutine(LoadScene("Level04-Test"));
        }
    }

   IEnumerator LoadScene(string sceneName)
   {
       fadein = true;
       yield return new WaitForSeconds(1);
       SceneManager.LoadScene(sceneName);
   }
}
