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

    bool fading;
    bool inMain;

    void Start()
    {
        fadeout = true;
        Debug.Log("went through");

        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "HelpMenu")
        {
            music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        }
        inMain = false;
        StartCoroutine(MusicFadeout());
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
                if(canvasGroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }
    IEnumerator MusicFadeIn()
    {
        while(music.volume > 0)
        {
            music.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MusicFadeout()
    {
        while(music.volume < 0.2f)
        {
            music.volume += 0.005f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void GoToMainMenu()
    {
        if(SceneManager.GetActiveScene().name == "HelpMenu")
        {
            inMain = true;
        }
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
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            inMain = true;
        }
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
        if (other.tag == "Exit" && SceneManager.GetActiveScene().name == "Level01-Test" && GameObject.FindGameObjectWithTag("coin") == null && GameObject.FindGameObjectWithTag("key") == null)
        {
            StartCoroutine(LoadScene("Level02-Test"));
        }
        else if(other.tag == "Exit" && SceneManager.GetActiveScene().name == "Level02-Test" && GameObject.FindGameObjectWithTag("coin") == null && GameObject.FindGameObjectWithTag("key") == null)
        {
            StartCoroutine(LoadScene("Level03-Test"));
           
        }
        else if(other.tag == "Exit" && SceneManager.GetActiveScene().name == "Level03-Test" && GameObject.FindGameObjectWithTag("coin") == null && GameObject.FindGameObjectWithTag("key") == null)
        {
            StartCoroutine(LoadScene("Level04-Test"));
        }
    }

   IEnumerator LoadScene(string sceneName)
   {
       fadein = true;
       if(!inMain)
       {
           StartCoroutine(MusicFadeIn());
       }
       yield return new WaitForSeconds(2);
       SceneManager.LoadScene(sceneName);
   }
}
