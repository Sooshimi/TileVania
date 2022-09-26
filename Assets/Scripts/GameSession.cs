using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages:
// Game sessions upon every death, and resets on 0 remaining lives
// Lives and Score counts

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score = 0;

    void Awake()
    {
        // find how many game sessions we have at start of game
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1) // destroy any new game sessions
        {
            Destroy(gameObject); // destroy new game session
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString(); // update score
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // get active scene index
        SceneManager.LoadScene(currentSceneIndex); // load active scene
        livesText.text = playerLives.ToString(); // update remaining lives
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist(); // destroy game persist to reset it
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
