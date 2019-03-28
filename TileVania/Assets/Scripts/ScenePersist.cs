using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{

    int startBuildIndex;

    private void Awake()
    {
        int nbOfScenePersistObj = FindObjectsOfType<ScenePersist>().Length;
        if(nbOfScenePersistObj > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        startBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    void Update()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentBuildIndex != startBuildIndex)
        {
            Destroy(gameObject);
        }
    }
}
