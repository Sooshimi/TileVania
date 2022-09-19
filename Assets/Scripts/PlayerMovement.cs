using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float jumpPower = 25f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
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
}
