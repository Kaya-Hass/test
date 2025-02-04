using UnityEngine;

public class KeyPuzzle : MonoBehaviour
{

    public GameObject KeyPuzzlePanel;
    public bool KeyPuzzleOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyPuzzlePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KeyPuzzleTag"))
        {
            KeyPuzzlePanel.SetActive(true);
            Debug.Log("Puzzle time, boy");
        }

        


    }
}
