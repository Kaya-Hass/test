using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public PatrolEnemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            enemy.inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            enemy.inRange = false;
        }
    }
}
