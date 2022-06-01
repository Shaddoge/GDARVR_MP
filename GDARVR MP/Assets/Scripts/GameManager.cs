using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get{ return instance; } }
    private bool existing = false;

    private int levelCleared = 0;
    public int LevelCleared { get{ return levelCleared; } }

    private int levelCurrent = 0;
    public int LevelCurrent { get{ return levelCurrent; } }

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            existing = true;
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        EventManager.Instance.OnCrystalCharged += CrystalCharged;
        EventManager.Instance.OnLevelChanged += LevelChanged;
    }

    private void CrystalCharged()
    {
        StartCoroutine(GameClearDelay(2));
    }

    private IEnumerator GameClearDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameClear();
    }

    private void GameClear()
    {
        // Update level cleared
        if(levelCleared < levelCurrent)
        {
            levelCleared = levelCurrent;
        }
        // Should call UI Manager to popup game over screen
        EventManager.Instance?.GameClear();
    }

    private void LevelChanged(int levelNum)
    {
        levelCurrent = levelNum;
    }

    private void OnDestroy()
    {
        if(!existing)
        {
            EventManager.Instance.OnCrystalCharged -= CrystalCharged;
            EventManager.Instance.OnLevelChanged -= LevelChanged;
        }
    }
}
