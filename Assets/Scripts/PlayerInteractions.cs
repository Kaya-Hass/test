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
    public GameObject puzzlePanel02P4;
    public GameObject puzzlePanel02P5;
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
    Vector3 initPiece04;
    Vector3 initPiece05;

    public GameObject piece01;
    public GameObject piece02;
    public GameObject piece03;
    public GameObject piece04;
    public GameObject piece05;

    bool inPuzzle;

    public PlayerMovement playerMovement;
    public AudioSource coinSFX;
    bool asleep;

    public Image fill;
    Color initial;
    Color red;

    bool low;

    public ParticleSystem speedFX;

    bool openDoor;
    public GameObject door;

    public GameObject keyPuzzle;
    public GameObject keyDisplay;
    public GameObject KeyPuzzlePanel;
    int numberOfSleeps;

    public GameObject plusEnemy01;
    public GameObject plusEnemy02;
    public GameObject plusEnemy03;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberOfSleeps = 0;
        keyDisplay.SetActive(false);
        if(SceneManager.GetActiveScene().name == "Level01-Test")
        {
            timer = 15f;
        }
        else if(SceneManager.GetActiveScene().name == "Level02-Test")
        {
            timer = 10f;
        }
        else if(SceneManager.GetActiveScene().name == "Level03-Test")
        {
            timer = 10f;
        }

        initPiece01 = piece01.gameObject.transform.position;
        initPiece02 = piece02.gameObject.transform.position;
        initPiece03 = piece03.gameObject.transform.position;
        if(SceneManager.GetActiveScene().name == "Level02-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
        {
            initPiece04 = piece04.gameObject.transform.position;
        }
        if(SceneManager.GetActiveScene().name == "Level03-Test")
        {
            initPiece05 = piece05.gameObject.transform.position;
        }

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
        KeyPuzzlePanel.SetActive(false);
        plusEnemy01.SetActive(false);

        if(SceneManager.GetActiveScene().name == "Level02-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
        {
            puzzlePanel02P4.SetActive(false);
            plusEnemy02.SetActive(false);
        }
        if(SceneManager.GetActiveScene().name == "Level03-Test")
        {
            puzzlePanel02P5.SetActive(false);
            plusEnemy03.SetActive(false);
        }
        numberOfCoins = 0;
        partOfPuzzle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(numberOfSleeps);
        if(SceneManager.GetActiveScene().name == "Level01-Test")
        {
            coinsText.text = numberOfCoins + "/3";
        }
        else if(SceneManager.GetActiveScene().name == "Level02-Test")
        {
            coinsText.text = numberOfCoins + "/6";
        }
        else if(SceneManager.GetActiveScene().name == "Level03-Test")
        {
            coinsText.text = numberOfCoins + "/5";
        }
        
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 5 && low != true)
            {
                low = true;
                StartCoroutine(Flash());
            }
        }
        else if(timer < 0)
        {
            timer = 0;
            low = false;
            asleep = true;
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
            if(SceneManager.GetActiveScene().name == "Level01-Test")
            {
                timer = 15f;
            }
            else if(SceneManager.GetActiveScene().name == "Level02-Test")
            {
                timer = 10f;
            }
            else if(SceneManager.GetActiveScene().name == "Level03-Test")
            {
                timer = 15f;
            }
            sleepBar.maxValue = timer;
        }

        if(Input.GetKeyDown(KeyCode.Space) && asleep == true)
        {
            safePanel.SetActive(false);
            asleep = false;
            if(SceneManager.GetActiveScene().name == "Level01-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
            {
                timer = 15f;
                sleepBar.maxValue = timer;
            }
            else if(SceneManager.GetActiveScene().name == "Level02-Test")
            {
                timer = 10f;
                sleepBar.maxValue = timer;
            }
        }

        if(numberOfSleeps == 1)
        {
            plusEnemy01.SetActive(true);
        }
        else if(numberOfSleeps == 3)
        {
            if(SceneManager.GetActiveScene().name == "Level02-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
            {
                plusEnemy02.SetActive(true);
            }
        }
        else if(numberOfSleeps == 5)
        {
            if(SceneManager.GetActiveScene().name == "Level03-Test")
            {
                plusEnemy03.SetActive(true);
            }
        }

    }

    IEnumerator Flash()
    {
        while(low == true)
        {
            ColorUtility.TryParseHtmlString("#FF0014", out red);
            fill.GetComponent<Image>().color = red;
            yield return new WaitForSeconds(0.8f);
            ColorUtility.TryParseHtmlString("#E5B780", out initial);
            fill.GetComponent<Image>().color = initial;
            yield return new WaitForSeconds(0.8f);

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

        if (collision.CompareTag("KeyPuzzleTag"))
        {
            KeyPuzzlePanel.SetActive(true);
            Debug.Log("Puzzle time, boy");
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
        Debug.Log("Checking");
        if(inSleepZone)
        {
            safePanel.SetActive(true);
            numberOfSleeps += 1;
            yield return new WaitForSeconds(5);
            safePanel.SetActive(false);
            if(SceneManager.GetActiveScene().name == "Level01-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
            {
                if(asleep == true)
                {
                    timer = 15f;
                    sleepBar.maxValue = timer;
                }
            }
            else if(SceneManager.GetActiveScene().name == "Level02-Test" && asleep == true)
            {
                if(asleep == true)
                {
                    timer = 10f;
                    sleepBar.maxValue = timer;
                }
            }
            asleep = false;
        }
        else if(inPuzzleZone && !inSleepZone)
        {
            yield return new WaitForSeconds(0);
            Debug.Log("inpuzzle checked");
            ChoosePuzzle();
        }
        else
        {
            yield return new WaitForSeconds(0);
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
            coinSFX.Play();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("key"))
        {
            keyDisplay.SetActive(true);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("speed"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(BoostSpeed());
        }

        if(collision.gameObject.CompareTag("time"))
        {
            timer += 4f;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Exit"))
        {
            if(GameObject.Find("coin") == null)
            {
                if(SceneManager.GetActiveScene().name == "Level01-Test")
                {
                    SceneManager.LoadScene("Level02-Test");
                }
                else if(SceneManager.GetActiveScene().name == "Level02-Test")
                {
                    SceneManager.LoadScene("Credits");
                }
            }
        }
    }

    IEnumerator BoostSpeed()
    {
        playerMovement.movementSpeed += 3f;
        speedFX.Play();
        yield return new WaitForSeconds(5);
        playerMovement.movementSpeed -= 3f;
        speedFX.Stop();

    }

    void ChoosePuzzle()
    {
        if(SceneManager.GetActiveScene().name == "Level01-Test" || SceneManager.GetActiveScene().name == "Level02-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
        {
            int randomPuzzle = Random.Range(1,4);
            Debug.Log("random chosen");

            switch(randomPuzzle)
            {
                case 1:
                puzzlePanel01.SetActive(true);
                Debug.Log("yes");
                puzzleTimerObject.SetActive(true);
                inPuzzle = true;
                if(SceneManager.GetActiveScene().name == "Level01-Test")
                {
                    puzzleTimer = 7f;
                }
                else if(SceneManager.GetActiveScene().name == "Level02-Test")
                {
                    puzzleTimer = 8f;
                }
                else if(SceneManager.GetActiveScene().name == "Level03-Test")
                {
                    puzzleTimer = 15f;
                }
                piece01.transform.position = initPiece01;
                piece02.transform.position = initPiece02;
                piece03.transform.position = initPiece03;

                if(SceneManager.GetActiveScene().name == "Level02-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
                {
                    piece04.transform.position = initPiece04;
                }
                if(SceneManager.GetActiveScene().name == "Level03-Test")
                {
                    piece05.transform.position = initPiece05;
                }
                break;

                case 2:
                inPuzzle = true;
                puzzlePanel02.SetActive(true);
                Debug.Log("yesu");
                puzzleTimerObject.SetActive(true);
                if(SceneManager.GetActiveScene().name == "Level01-Test")
                {
                    puzzleTimer = 5f;
                }
                else if(SceneManager.GetActiveScene().name == "Level02-Test")
                {
                    puzzleTimer = 7f;
                }
                else if(SceneManager.GetActiveScene().name == "Level03-Test")
                {
                    puzzleTimer = 9f;
                }
                partOfPuzzle ++;
                break;

                case 3:
                inPuzzle = true;
                puzzlePanel03.SetActive(true);
                Debug.Log("yeso");
                puzzleTimerObject.SetActive(true);
                door01.SetActive(true);
                door02.SetActive(true);
                door03.SetActive(true);
                puzzleTimer = 5f;
                break;
            }
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
            if(SceneManager.GetActiveScene().name == "Level01-Test")
            {
                puzzlePanel02P3.SetActive(false);
                puzzleTimerObject.SetActive(false);
                inPuzzle = false;
                partOfPuzzle = 0;
                timer = 15f;
                sleepBar.maxValue = timer;
            }
            else if(SceneManager.GetActiveScene().name == "Level02-Test" || SceneManager.GetActiveScene().name == "Level03-Test")
            {
                puzzlePanel02P3.SetActive(false);
                puzzlePanel02P4.SetActive(true);
                partOfPuzzle ++;
            }
        }
        else if(partOfPuzzle == 4)
        {
            if(SceneManager.GetActiveScene().name == "Level02-Test")
            {
                puzzlePanel02P4.SetActive(false);
                puzzleTimerObject.SetActive(false);
                inPuzzle = false;
                partOfPuzzle = 0;
                timer = 10f;
                sleepBar.maxValue = timer;
            }
            else if(SceneManager.GetActiveScene().name == "Level03-Test")
            {
                puzzlePanel02P4.SetActive(false);
                puzzlePanel02P5.SetActive(true);
                partOfPuzzle ++;
            }
        }
        else if(partOfPuzzle == 5)
        {
            puzzlePanel02P5.SetActive(false);
            puzzleTimerObject.SetActive(false);
            inPuzzle = false;
            partOfPuzzle = 0;
            timer = 10f;
            sleepBar.maxValue = timer;
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
        inPuzzle = false;
        StartCoroutine(DisplayEnemy());
    }

    public void PressSafeDoor()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        puzzleTimerObject.SetActive(false);
        inPuzzle = false;
        StartCoroutine(DisplaySafe());
    }

    public void FindObject()
    {
        Destroy(door);
        keyPuzzle.SetActive(false);
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
        if(SceneManager.GetActiveScene().name == "Level01-Test")
        {
            timer = 15f;
        }
        else if(SceneManager.GetActiveScene().name == "Level02-Test")
        {
            timer = 10f;
        }
        else if(SceneManager.GetActiveScene().name == "Level03-Test")
        {
            timer = 15f;
        }
        sleepBar.maxValue = timer;
    }
}
