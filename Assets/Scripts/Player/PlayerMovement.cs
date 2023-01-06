using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float JumpForce = 14.0f;
    private float Move;

    public bool IsGrounded = true;
    public bool ShouldJump;
    public bool CanStand = true;
    public bool IsSprinting;

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
        if(Input.GetButton("Sprint") && IsGrounded && CanStand)
        {
            MoveSpeed = 1.3f;
            IsSprinting = true;
        }
        else if (Input.GetButtonUp("Sprint") || !IsGrounded)
        {
            MoveSpeed = 1.0f;
            IsSprinting = false;
        }
    }



    //Jumping

    void JumpInput()
    {
        if (Input.GetButton("Jump") && IsGrounded && CanStand)
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
        if(Input.GetButton("Crouch") && !IsSprinting && IsGrounded)
        {
            CrouchCollider.enabled = false;
            MoveSpeed = 0.7f;
        }
        else if(CanStand && !IsSprinting) 
        {
            CrouchCollider.enabled = true;
            MoveSpeed = 1.0f;
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
            CanStand = false;
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
            CanStand = true;
        }
    }
}