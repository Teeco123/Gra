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
    public bool FacingRight = true;

    public Rigidbody2D Rigidbody;
    public BoxCollider2D CrouchCollider;
    public Animator Animator;



    void Update()
    {
        MovementInput();
        JumpInput();
        Sprint();
        Crouch();
        FlipInput();
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

        Animator.SetFloat("Speed", Mathf.Abs(Move));
    }

    void Movement()
    {
        Rigidbody.velocity =  new Vector2(Move * MoveSpeed, Rigidbody.velocity.y);
    }

    void Sprint()
    {
        if(Input.GetButton("Sprint") && IsGrounded && CanStand)
        {
            MoveSpeed = 1.5f;
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
        if (Input.GetButtonDown("Jump") && IsGrounded && CanStand)
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



    //Flip

    void FlipInput()
    {
        if(Move > 0 && !FacingRight)
        {
            Flip();
        }
        else if(Move < 0 && FacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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