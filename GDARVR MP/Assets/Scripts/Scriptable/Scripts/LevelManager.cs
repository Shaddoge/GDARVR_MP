using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Manager", menuName = "Level Manager")]
public class LevelManager : ScriptableObject
{
    public Level currentLevel;
    public Level[] levelList;
    private Level latestLevelClear;

    private void OnEnable()
    {
        Debug.Log("Level Manager Enabled");
    }

    public void LevelFinished(LevelClearData levelClearData)
    {
        
        // If next level exists
        if (currentLevel.nextLevel)
        {
            if (currentLevel.highscore < levelClearData.highScore)
                currentLevel.highscore = levelClearData.highScore;

            // Unlock level
            if (currentLevel.nextLevel.isLocked)
                currentLevel.nextLevel.isLocked = false;
        }
        EventManager.Instance?.GameClear(levelClearData);
    }

    public void NextLevel()
    {
        if (currentLevel == null) return;
        Level levelToLoad = currentLevel.nextLevel;

        SCENE_MANAGER.Instance?.LoadLevel(levelToLoad);
    }

    public void StartGame()
    {
        Level levelToLoad = new Level();

        for (int i = 0; i < levelList.Length; i++)
        {
            if (levelList[i].nextLevel)
            {
                if (levelList[i].nextLevel.isLocked)
                {
                    levelToLoad = levelList[i];
                    break;
                }
            }
            else
            {
                levelToLoad = levelList[i];
                break;
            }
        }

        SCENE_MANAGER.Instance?.LoadLevel(levelToLoad);
    }

    public int GetLatestLevelClear()
    {
        return 0;
    }

    private void OnDisable()
    {
        Debug.Log("Level Manager Disabled");
    }
}
