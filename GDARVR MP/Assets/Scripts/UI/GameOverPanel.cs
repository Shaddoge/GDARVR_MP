using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void LevelSelect()
    {
        EventManager.Instance?.LevelSelect();
    }

    public void NextLevel()
    {
        EventManager.Instance?.NextLevelClick();
    }
}
