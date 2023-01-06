using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float JumpForce = 14.0f;
    private float Move;

    private bool IsGrounded = true;
    private bool ShouldJump;
    private bool CanCrouch = true;

    public Rigidbody2D Rigidbody;
    public BoxCollider2D CrouchCollider;



    void Update()
    {
        MovementInput();
        JumpInput();
        Sprint();
        Crouch();
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

    void Sprint()
    {
        if(Input.GetButton("Sprint"))
        {
            MoveSpeed = 1.3f;
        }
        else
        {
            MoveSpeed = 1.0f;
        }
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

    //Crouching

    void Crouch()
    {
        if(Input.GetButton("Crouch") && CanCrouch)
        {
            CrouchCollider.enabled = false;
        }
        else
        {
            CrouchCollider.enabled = true;
        }
    }



    //Ground Check

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            IsGrounded = true;
        }
        if(collision.gameObject.tag == "Ceiling")
        {
            CanCrouch = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            IsGrounded = false;
        }
        if (collision.gameObject.tag == "Ceiling")
        {
            CanCrouch = true;
        }
    }
}