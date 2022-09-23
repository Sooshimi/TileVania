using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed = 10f;
    PlayerMovement player; // references player. Player is only object with PlayerMovement component
    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (xSpeed, 0f);
        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject); // destroys enemy
        }
        Destroy(gameObject); // destroys bullet
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject); // destroys bullet
    }
}
