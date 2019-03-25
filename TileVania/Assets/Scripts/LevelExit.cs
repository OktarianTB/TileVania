using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    float levelLoadDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (!boxCollider)
        {
            Debug.LogWarning("Box Collider is missing on Level Exit game object");
            return;
        }
        bool isPlayer = boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"));

        if (isPlayer)
        {
            StartCoroutine(WaitUntilNextSceneLoad());
        }
    }

    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCountInBuildSettings < currentScene + 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    IEnumerator WaitUntilNextSceneLoad()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        LoadNextScene();
    }

}
