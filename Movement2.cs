using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpStrength = 2f;
    public float gravitonus = 0.5f;
    public Rigidbody2D rb;
    private bool isGrounded = true;
    private bool jump = false;

    //Ground check via box collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Update()
    { 
        Debug.Log("Grounded = " + isGrounded);
        Debug.Log("Jump = " + jump); //Minor checks
    }
    //Physics involved motion
    private void FixedUpdate()
    {
        //Button assignment
        if (Input.GetButton("Vertical") || Input.GetButton("Jump"))
        {
            jump = true;
        }
        else
        {
            jump = false;

        }

        //Horizontal motion
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        //Jump motion
        if ((jump == true) && (isGrounded == true))
        {
            if (rb.velocity.y == 0) //To prevent "wall" bounding

            {
                rb.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
                /// Scrapped code:
                ///     isGrounded = false
            }
        }

        //Gliding
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.gravityScale = gravitonus;
        }
        else
        {
            rb.gravityScale = 1.5f;
        }


    }









}
