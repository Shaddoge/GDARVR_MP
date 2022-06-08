using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENE_MANAGER : MonoBehaviour
{
    // Singleton

    public static SCENE_MANAGER instance = null;
    private bool existing = false;

    private void Awake()
    {
        if (instance != null)
        {
            existing = true;
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        EventManager.Instance.OnNextLevel += LoadSelectedLevel;
        EventManager.Instance.OnLevelSelect += OpenLevelSelect;
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
        if (GameManager.Instance.LevelCleared == 0)
        {
            GameManager.Instance.levelCurrent = 1;
        }
        Debug.Log("Curent Level: "+ GameManager.Instance.levelCurrent);
        SceneManager.LoadScene("test");
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

    private void OnDestroy()
    {
        if(!existing)
        {
            EventManager.Instance.OnNextLevel -= LoadSelectedLevel;
            EventManager.Instance.OnLevelSelect -= OpenLevelSelect;
        }
    }
}
