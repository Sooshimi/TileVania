using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        // find how many game sessions we have at start of game
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;

        if (numScenePersists > 1) // destroy any new game sessions
        {
            Destroy(gameObject); // destroy new game session
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
