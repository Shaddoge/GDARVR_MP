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
    [HideInInspector] public int mirrorsUsed = 0;

    private bool timerStarted = false;
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
        EventManager.Instance.OnPlatformDetected += StartTime;
        EventManager.Instance.OnCrystalCharged += CrystalCharged;
        EventManager.Instance.OnLevelChanged += LevelChanged;
    }

    private void Update()
    {
        if (!timerStarted || levelEnded) return;
        levelTime += Time.deltaTime;
        //Debug.Log(levelTime);
        if(MenuHUD.Instance)
        {
            MenuHUD.Instance?.UpdateTime(levelTime);
        }
    }

    private void StartTime(bool isDetected)
    {
        if(timerStarted) return;
        
        if(isDetected)
            timerStarted = true;
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
        levelClearData.mirrorsUsed = mirrorsUsed;
        levelClearData.highScore = levelManager.currentLevel.highscore;
        levelClearData.CalculateScore((int)levelTime, mirrorsUsed);

        levelManager.LevelFinished(levelClearData);
        //EventManager.Instance?.GameClear((int)levelTime);
    }

    private void LevelChanged(Level newLevel)
    {
        levelEnded = false;
        timerStarted = false;
        levelTime = 0;

        levelManager.currentLevel = newLevel;
    }

    public void UpdateMirrorsUsed(int num)
    {
        if (levelEnded) return;
        mirrorsUsed = num;
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
