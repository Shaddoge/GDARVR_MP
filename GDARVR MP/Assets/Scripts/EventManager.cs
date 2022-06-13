using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    private bool existing = false;
    public static EventManager Instance { get{ return instance; } }

    public event Action<bool> OnPlatformTracked;
    public event Action<bool> OnMirrorTracked;

    public event Action OnCrystalCharged;
    public event Action OnRestart, OnGameClear;

    public event Action OnLevelSelect;

    public event Action OnNextLevelClick;
    public event Action<int> OnNextLevel;

    public event Action<int> OnLevelChanged;

    public event Action<int> OnUnlockLevel;

    // Start is called before the first frame update
    void Awake()
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
    }

    public void PlatformTracked(bool isTracked)
    {
        if(OnPlatformTracked != null)
        {
            OnPlatformTracked(isTracked);
        }
    }

    public void MirrorTracked(bool isTracked)
    {
        if(OnMirrorTracked != null)
        {
            OnMirrorTracked(isTracked);
        }
    }

    public void CrystalCharged()
    {
        if(OnCrystalCharged != null)
        {
            OnCrystalCharged();
        }
    }

    public void GameClear()
    {
        if(OnGameClear != null)
        {
            OnGameClear();
        }
    }

    public void LevelSelect()
    {
        if(OnLevelSelect != null)
        {
            OnLevelSelect();
        }
    }

    public void NextLevelClick()
    {
        if(OnNextLevelClick != null)
        {
            OnNextLevelClick();
        }
    }

    public void NextLevel(int levelNum)
    {
        if(OnNextLevel != null)
        {
            OnNextLevel(levelNum);
        }
    }

    public void LevelChanged(int levelNum)
    {
        if(OnLevelChanged != null)
        {
            OnLevelChanged(levelNum);
        }
    }

    public void UnlockLevels(int nLevelsUnlocked)
    {
        if (OnUnlockLevel != null)
        {
            OnUnlockLevel(nLevelsUnlocked);
        }
    }
}
