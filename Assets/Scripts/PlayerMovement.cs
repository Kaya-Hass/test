using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    Animator animator;
    AudioSource sfx;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("backward", true);
            sfx.volume = 0.3f;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("left", true);
            sfx.volume = 0.3f;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("forward", true);
            sfx.volume = 0.3f;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("right", true);
            sfx.volume = 0.3f;
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("backward", false);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("left", false);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("forward", false);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("right", false);
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            sfx.volume = 0;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * movementSpeed;
    }
}
