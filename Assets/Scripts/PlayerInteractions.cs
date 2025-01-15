using UnityEngine;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    bool inSleepZone;
    bool inPuzzleZone;
    public TMP_Text timerText;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
}
