using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay); // wait 1 second
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // get active scene index
        int nextSceneIndex = currentSceneIndex + 1; // checks next scene index (in case the current one is the last)

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // if there's no new scene next
        {
            nextSceneIndex = 0; // load scene 0 again
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist(); // destroy game persist so that it doesn't persist to the next level
        SceneManager.LoadScene(nextSceneIndex); // load next scene
    }
}
