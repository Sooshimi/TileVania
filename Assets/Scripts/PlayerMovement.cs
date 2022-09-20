using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float jumpPower = 25f;
    [SerializeField] float climbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    float gravityScaleAtStart;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // first get layer mask of "Ground", then use that layer mask as parameter for IsTouchingLayers() function
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return ;}

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpPower);
        }
    }

    void Run()
    {
        // keep velocity y as it currently is, to prevent character from slowly floating downwards
        Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        // check if player is moving (0 is technically ~0.000001, so player would flip to face right whenever idle)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        // check if player is moving (0 is technically ~0.000001, so player would flip to face right whenever idle)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; // Mathf.Epsilon, instead of 0

        if (playerHasHorizontalSpeed) 
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart; // normal gravity when not climbing ladder
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f; // no gravity when climbing ladder

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
}
