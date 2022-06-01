using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENE_MANAGER : MonoBehaviour
{
    // Singleton

    public static SCENE_MANAGER instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenNewGame()
    {
        // start new game
        // locks all levels

        SceneManager.LoadScene("Main");
        Debug.Log("New Game");
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelection");
        Debug.Log("Opened Level Selection Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
        Debug.Log("Mainmenu");
    }

    public void LoadSelectedLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
