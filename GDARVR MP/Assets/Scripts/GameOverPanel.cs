using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LevelSelect()
    {
        EventManager.Instance?.LevelSelect();
    }

    public void NextLevel()
    {
        EventManager.Instance?.NextLevelClick();
    }
}
