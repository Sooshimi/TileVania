using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinPoints = 100;

    bool coinCollected = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !coinCollected) // condition on coinCollected to prevent double pickup
        {
            coinCollected = true;
            FindObjectOfType<GameSession>().AddToScore(coinPoints); // calling a GameSession public method to add to score
            // play audio at point, and the audio position is the camera itself (for full audio at any position)
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false); // deactive coin gameObject to make sure it's definitely deactivated upon pickup
            Destroy(gameObject);
        }
    }
}
