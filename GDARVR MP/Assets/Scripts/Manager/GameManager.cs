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

    public int levelCurrent = 0;

    private float levelTime = 0f;

    //public int LevelCurrent { get{ return levelCurrent; } }

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            existing = true;
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        EventManager.Instance.OnCrystalCharged += CrystalCharged;
        EventManager.Instance.OnNextLevelClick += PlayNextLevel;
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
        levelTime = 0;
    }

    private void PlayNextLevel()
    {
        Debug.Log($"Level Current: {levelCurrent}");
        string nextLevelName = "Level " + (levelCurrent + 1).ToString();
        EventManager.Instance?.NextLevel(levelCurrent + 1);
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
