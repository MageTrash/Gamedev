using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpStrength = 2f;
    public float swimSpeed = 10f;
    public float swimDrag = 5f;
    public float gravitonus = 0.5f;
    public float swimitonus = 0.1f;
    public Rigidbody2D rb;
    private bool jump = false;
    private bool isGrounded = true;
    private bool isSwimming = false;

    //Ground check and Swimming check
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Liquid"))
        {
            isSwimming = true;
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        //Swimming
        if (other.gameObject.CompareTag("Liquid"))
        {
            isSwimming = false;
        }
    }

    private void Update()
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
        if (isSwimming == false)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            //transform.position += movement * Time.deltaTime * moveSpeed;
            transform.Translate(movement * Time.deltaTime * moveSpeed);
        }
        //Debug checks
        Debug.Log("Swimming = " + isSwimming);
        Debug.Log("Grounded = " + isGrounded);
        Debug.Log("Jump = " + jump);
    }

    //Physics involved motion
    private void FixedUpdate()
    {
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
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (rb.velocity.y < -2f) && (isSwimming == false))
        {
            rb.gravityScale = gravitonus;


        }
        //Swimming
        else if (isSwimming == true)
        {
            rb.velocity = new Vector2(0f, -swimitonus);
            float moHorz = Input.GetAxis("Horizontal");
            float moVert = Input.GetAxis("Vertical");
            Vector2 swimMovement = new Vector2(moHorz, moVert);
            //rb.AddForce(swimMovement * swimSpeed);

            //transform.position += movement * Time.deltaTime * swimSpeed;
            transform.Translate(swimMovement * swimSpeed / swimDrag);

            isGrounded = false;
        }
        else
        {
            rb.gravityScale = 1.5f;
        }
    }
}
