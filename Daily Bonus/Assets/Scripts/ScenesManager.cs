using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private Text SceneName;

    private static ScenesManager _instance = null;
    public static ScenesManager Instance
    {
        get { return _instance; }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void LoadNextScene()
    {
        
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {

            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Scena : " + SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}
