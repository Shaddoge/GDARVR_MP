using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Manager", menuName = "Level Manager")]
public class LevelManager : ScriptableObject
{
    public Level currentLevel;
    public Level[] levelList;

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

    public Level GetLatestLevel()
    {
        for (int i = 0; i < levelList.Length; i++)
        {
            if (levelList[i].nextLevel)
                if (levelList[i].nextLevel.isLocked)
                    return levelList[i];
            else
                return levelList[i];
        }

        return null;
    }

    public void ResetLevelData()
    {
        AudioManager.Instance.PlayButtonSFX();
        for (int i = 0; i < levelList.Length; i++)
        {
            Level level = levelList[i];
            level.highscore = 0;

            if(i > 0)
            {
                level.isLocked = true;
            }
            else
            {
                currentLevel = level;
            }
        }
    }
    private void OnDisable()
    {
        Debug.Log("Level Manager Disabled");
    }
}
