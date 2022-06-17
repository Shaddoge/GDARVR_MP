using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    public static EventManager Instance { get{ return instance; } }

    public event Action<bool> OnPlatformTracked;
    public event Action<int, bool> OnToggleLock;

    public event Action OnCrystalCharged;
    public event Action<LevelClearData> OnGameClear;

    public event Action OnLevelSelect;

    public event Action OnNextLevelClick;
    public event Action OnNextLevel;

    public event Action<Level> OnLevelChanged;

    //public event Action<int> OnUnlockLevel;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlatformTracked(bool isTracked)
    {
        if(OnPlatformTracked != null)
        {
            OnPlatformTracked(isTracked);
        }
    }

    public void ToggleLocked(int index, bool locked)
    {
        if(OnToggleLock != null)
        {
            OnToggleLock(index, locked);
        }
    }

    public void CrystalCharged()
    {
        if(OnCrystalCharged != null)
        {
            OnCrystalCharged();
        }
    }

    public void GameClear(LevelClearData levelClearData)
    {
        if(OnGameClear != null)
        {
            OnGameClear(levelClearData);
        }
    }

    public void LevelSelect()
    {
        if(OnLevelSelect != null)
        {
            OnLevelSelect();
        }
    }

    public void NextLevel()
    {
        if(OnNextLevel != null)
        {
            OnNextLevel();
        }
    }

    public void LevelChanged(Level newLevel)
    {
        if(OnLevelChanged != null)
        {
            OnLevelChanged(newLevel);
        }
    }

    /*public void UnlockLevels(int nLevelsUnlocked)
    {
        if (OnUnlockLevel != null)
        {
            OnUnlockLevel(nLevelsUnlocked);
        }
    }*/
}
