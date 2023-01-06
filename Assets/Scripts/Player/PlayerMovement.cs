using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;
    public float Move;

    private bool IsGrounded = true;
    private bool ShouldJump;

    public Rigidbody2D Rigidbody;



    void Update()
    {
        MovementInput();
        JumpInput();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }



    //Movement

    void MovementInput()
    {
        Move = Input.GetAxisRaw("Horizontal");
    }

    void Movement()
    {
        Rigidbody.velocity =  new Vector2(Move * MoveSpeed, Rigidbody.velocity.y);
    }



    //Jumping

    void JumpInput()
    {
        if (Input.GetButton("Jump") && IsGrounded)
        {
            ShouldJump = true;
        }
        else
        {
            ShouldJump = false;
        }
    }

    void Jump()
    {
        if(ShouldJump)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, JumpForce);
        }
    }



    //Ground Check

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            IsGrounded = true;
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            IsGrounded = false;
        }
    }
}