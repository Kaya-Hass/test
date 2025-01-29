using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolEnemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject enemy;
    Rigidbody2D rb;
    public float speed;

    public GameObject player;

    private float distance;

    public bool inRange;
    bool pointApassed;
    Animator animator;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = pointB.transform.position;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!inRange)
        {
            animator.SetBool("chasing", false);
            if(Vector2.Distance(transform.position, pointB.transform.position) < 0.1f && !inRange)
            {
                pointApassed = false;
            }
            else if(Vector2.Distance(transform.position, pointA.transform.position) < 0.1f && !inRange)
            {
                pointApassed = true;
            }

            if(!pointApassed)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, pointA.transform.position, speed * Time.deltaTime);
                animator.SetBool("goingA", true);
                
            }
            else if(pointApassed)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, pointB.transform.position, speed * Time.deltaTime);
                animator.SetBool("goingA", false);
            }
        }
        else if(inRange)
        {
            animator.SetBool("chasing", true);
            Vector2 direction = player.transform.position - transform.position;
            distance = Vector2.Distance(transform.position, player.transform.position);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

}
