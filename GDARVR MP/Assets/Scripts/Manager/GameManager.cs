using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get{ return instance; } }
    private bool existing = false;

    [SerializeField] private LevelManager levelManager;

    private float levelTime = 0f;
    private bool levelEnded = false;

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

    private void Update()
    {
        if (levelEnded) return;
        levelTime += Time.deltaTime;
        //Debug.Log(levelTime);
        if(MenuHUD.Instance)
        {
            MenuHUD.Instance?.UpdateTime(levelTime);
        }
    }

    private void CrystalCharged()
    {
        levelEnded = true;
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
        /*if(levelCleared < levelCurrent)
        {
            levelCleared = levelCurrent;
        }*/

        // Should call UI Manager to popup game over screen
        
        LevelClearData levelClearData = new LevelClearData();
        
        levelClearData.time = (int)levelTime;
        levelClearData.mirrorsPlaced = 1;
        levelClearData.highScore = levelManager.currentLevel.highscore;
        levelClearData.CalculateScore((int)levelTime, 1);

        levelManager.LevelFinished(levelClearData);
        //EventManager.Instance?.GameClear((int)levelTime);
    }

    private void LevelChanged(Level newLevel)
    {
        levelEnded = false;
        levelTime = 0;

        levelManager.currentLevel = newLevel;
    }

    private void PlayNextLevel()
    {
        Debug.Log($"Level Current: {levelManager.currentLevel}");
        levelManager.NextLevel();

        //EventManager.Instance?.NextLevel(levelCurrent + 1);
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
