using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    AudioSource music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "HelpMenu" || SceneManager.GetActiveScene().name == "Level04-Test")
        {
            music.volume = 1;
        }
        else
        {
            music.volume = 0;
        }
    }

    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        if(obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
