using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SCENE_MANAGER.Instance?.OpenNewGame();
    }

    public void LevelSelect()
    {
        SCENE_MANAGER.Instance?.OpenLevelSelect();
    }

    public void QuitGame()
    {
        SCENE_MANAGER.Instance?.QuitGame();
    }
}
