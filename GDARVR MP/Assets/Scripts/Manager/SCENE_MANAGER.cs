using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENE_MANAGER : MonoBehaviour
{
    // Singleton

    private static SCENE_MANAGER instance = null;
    public static SCENE_MANAGER Instance { get{ return instance; } }

    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("Destroying");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OpenNewGame()
    {
        // start new game
        // locks all levels
        AudioManager.Instance.PlayButtonSFX();
        Debug.Log("Current Level: " + levelManager.currentLevel);

        //Level level = levelManager.GetLatestLevel();
        Level level = levelManager.currentLevel;
        Debug.Log(level.name);
        if (level == null) return;
        LoadLevel(level);

        /*if (GameManager.Instance.LevelCleared == 0)
        {
            GameManager.Instance.levelCurrent = 1;
        }
        LoadLevelByNum(GameManager.Instance?.levelCurrent);
        Debug.Log("New Game");*/
    }

    public void OpenLevelSelect()
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene("LevelSelection");
        Debug.Log("Opened Level Selection Menu");

    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonSFX();
        Application.Quit();
        Debug.Log("Application Closed");
    }

    public void OpenMainMenu()
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene("Mainmenu");
        Debug.Log("Mainmenu");
    }

    public void LoadLevel(Level selectedLevel)
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene(selectedLevel.SceneName);
        EventManager.Instance?.LevelChanged(selectedLevel);
    }

    public void LoadNextLevel()
    {
        AudioManager.Instance.PlayButtonSFX();
        Level levelToLoad = levelManager.currentLevel.nextLevel;
        SceneManager.LoadScene(levelToLoad.SceneName);
        EventManager.Instance?.LevelChanged(levelToLoad);
    }

    private void OnDestroy()
    {
    }
}
