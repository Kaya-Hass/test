using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInteractions : MonoBehaviour
{
    bool inSleepZone;
    bool inPuzzleZone;
    public float timer;
    public float puzzleTimer;
    public GameObject puzzleTimerObject;
    public TMP_Text puzzleTimerText;
    public TMP_Text coinsText;

    public GameObject deathPanel;
    public GameObject safePanel;
    public GameObject puzzlePanel01;
    public GameObject puzzlePanel02;
    public GameObject puzzlePanel02P2;
    public GameObject puzzlePanel02P3;
    public GameObject puzzlePanel03;
    public GameObject capturePanel;
    public GameObject door01;
    public GameObject door02;
    public GameObject door03;
    public Slider sleepBar;

    public GameObject camera;

    public int puzzle01;
    public int puzzle02;
    public int puzzle03;

    static int numberOfCoins;
    int partOfPuzzle;

    Vector3 initPiece01;
    Vector3 initPiece02;
    Vector3 initPiece03;

    public GameObject piece01;
    public GameObject piece02;
    public GameObject piece03;

    bool inPuzzle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puzzleTimerObject.SetActive(false);
        sleepBar.maxValue = timer;
        deathPanel.SetActive(false);
        safePanel.SetActive(false);
        puzzlePanel01.SetActive(false);
        puzzlePanel02P2.SetActive(false);
        puzzlePanel02P3.SetActive(false);
        puzzlePanel02.SetActive(false);
        puzzlePanel03.SetActive(false);
        capturePanel.SetActive(false);
        if(SceneManager.GetActiveScene().name == "Level01-Test")
        {
            numberOfCoins = 0;
        }
        partOfPuzzle = 0;
        initPiece01 = piece01.gameObject.transform.position;
        initPiece02 = piece02.gameObject.transform.position;
        initPiece03 = piece03.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "" + numberOfCoins;
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 0;
            StartCoroutine(CheckZone());
        }

        if(inPuzzle)
        {
            if(puzzleTimer > 0)
            {
                puzzleTimer -= Time.deltaTime;
            }
            else if(puzzleTimer < 0)
            {
                puzzleTimer = 0;
                capturePanel.SetActive(true);
            }

            int minutes = Mathf.FloorToInt(puzzleTimer/60);
            int seconds = Mathf.FloorToInt(puzzleTimer % 60);
            puzzleTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        sleepBar.value = timer;

        if(puzzle01 >= 3 || puzzle02 >= 4 || puzzle03 >= 5)
        {
            puzzlePanel01.SetActive(false);
            puzzleTimerObject.SetActive(false);
            inPuzzle = false;
            puzzle01 = 0;
            puzzle02 = 0;
            puzzle03 = 0;
            timer = 10f;
        }

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

    IEnumerator CheckZone()
    {
        if(inSleepZone)
        {
            safePanel.SetActive(true);
            yield return new WaitForSeconds(5);
            safePanel.SetActive(false);
            timer = 5f;
            sleepBar.maxValue = timer;
        }
        else if(inPuzzleZone && !inSleepZone)
        {
            yield return new WaitForSeconds(0);
            ChoosePuzzle();
        }
        else
        {
            yield return new WaitForSeconds(2);
            deathPanel.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("touched");
            capturePanel.SetActive(true);
        }

        if(collision.gameObject.CompareTag("coin"))
        {
            numberOfCoins ++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Exit"))
        {
            if(GameObject.Find("coin") == null)
            {
                SceneManager.LoadScene("Level02-Test");
            }
        }
    }

    void ChoosePuzzle()
    {
        StopCoroutine(CheckZone());
        if(SceneManager.GetActiveScene().name == "Level01-Test")
        {
            int randomPuzzle = Random.Range(1,4);

            switch(randomPuzzle)
            {
                case 1:
                puzzlePanel01.SetActive(true);
                puzzleTimerObject.SetActive(true);
                inPuzzle = true;
                puzzleTimer = 7f;
                piece01.transform.position = initPiece01;
                piece02.transform.position = initPiece02;
                piece03.transform.position = initPiece03;
                break;

                case 2:
                inPuzzle = true;
                puzzlePanel02.SetActive(true);
                puzzleTimerObject.SetActive(true);
                puzzleTimer = 5f;
                partOfPuzzle ++;
                break;

                case 3:
                inPuzzle = true;
                puzzlePanel03.SetActive(true);
                puzzleTimerObject.SetActive(true);
                door01.SetActive(true);
                door02.SetActive(true);
                door03.SetActive(true);
                puzzleTimer = 5f;
                break;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Level02-Test")
        {

        }
    }

    public void PressRightHeart()
    {
        if(partOfPuzzle == 1)
        {
            puzzlePanel02P2.SetActive(true);
            puzzlePanel02.SetActive(false);
            partOfPuzzle ++;
        }
        else if(partOfPuzzle == 2)
        {
            puzzlePanel02P3.SetActive(true);
            puzzlePanel02P2.SetActive(false);
            partOfPuzzle ++;
        }
        else if(partOfPuzzle == 3)
        {
            puzzlePanel02P3.SetActive(false);
            puzzleTimerObject.SetActive(false);
            inPuzzle = false;
            timer = 10f;
            sleepBar.maxValue = timer;
            partOfPuzzle = 0;
        }
        
    }

    public void PressWrongHeart()
    {
        capturePanel.SetActive(true);
        puzzlePanel02P2.SetActive(false);
        puzzlePanel02P3.SetActive(false);
        puzzlePanel02.SetActive(false);
        puzzleTimerObject.SetActive(false);
        inPuzzle = false;
    }

    public void PressEnemyDoor()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        puzzleTimerObject.SetActive(false);
        StartCoroutine(DisplayEnemy());
    }

    public void PressSafeDoor()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        puzzleTimerObject.SetActive(false);
        StartCoroutine(DisplaySafe());
    }

    IEnumerator DisplayEnemy()
    {
        yield return new WaitForSeconds(3);
        puzzlePanel03.SetActive(false);
        capturePanel.SetActive(true);
        inPuzzle = false;
    }

    IEnumerator DisplaySafe()
    {
        yield return new WaitForSeconds(3);
        puzzlePanel03.SetActive(false);
        inPuzzle = false;
        timer = 10f;
        sleepBar.maxValue = timer;
    }
}
