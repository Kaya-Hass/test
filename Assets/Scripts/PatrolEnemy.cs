using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject enemy;
    Rigidbody2D rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = pointB.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("UpDown"))
        {
            if(Vector2.Distance(transform.position, pointB.transform.position) < 0.1f)
            {
                rb.linearVelocity = new Vector2(0, -speed);
            }
            else if(Vector2.Distance(transform.position, pointA.transform.position) < 0.1f)
            {
                rb.linearVelocity = new Vector2(0, speed);
            }
        }
        else if(gameObject.CompareTag("LeftRight"))
        {
            if(Vector2.Distance(transform.position, pointB.transform.position) < 0.1f)
            {
                rb.linearVelocity = new Vector2(speed, 0);
            }
            else if(Vector2.Distance(transform.position, pointA.transform.position) < 0.1f)
            {
                rb.linearVelocity = new Vector2(-speed, 0);
            }
        }


    }
}
