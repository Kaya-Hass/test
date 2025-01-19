using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    bool inSleepZone;
    bool inPuzzleZone;
    public float timer;

    public GameObject deathPanel;
    public GameObject safePanel;
    public GameObject puzzlePanel;
    public Slider sleepBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathPanel.SetActive(false);
        safePanel.SetActive(false);
        puzzlePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 0;
            StartCoroutine(CheckZone());
        }
        sleepBar.value = timer;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Sleep"))
        {
            Debug.Log("SleepZone");
            inSleepZone = true;
        }

        if(collision.CompareTag("Puzzle"))
        {
            Debug.Log("PuzzleZone");
            inPuzzleZone = true;
        }

        if(collision.CompareTag("Exit"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Sleep"))
        {
            inSleepZone = false;
        }

        if(collision.CompareTag("Puzzle"))
        {
            inPuzzleZone = false;
        }
    }

    IEnumerator CheckZone()
    {
        if(inSleepZone)
        {
            safePanel.SetActive(true);
            yield return new WaitForSeconds(5);
            safePanel.SetActive(false);
            timer = 5f;
        }
        else if(inPuzzleZone && !inSleepZone)
        {
            puzzlePanel.SetActive(true);
            yield return new WaitForSeconds(3);
            puzzlePanel.SetActive(false);
            timer = 5f;
        }
        else
        {
            yield return new WaitForSeconds(2);
            deathPanel.SetActive(true);
        }
    }
}
