using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    public static EventManager Instance { get{ return instance; } }

    public event Action<bool> OnPlatformDetected;
    public event Action<int> OnInitializeMirrors;
    public event Action<int, bool> OnToggleLock;
    public event Action OnResetMirrors;

    public event Action OnCrystalCharged;
    public event Action<LevelClearData> OnGameClear;

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

    public void PlatformDetected(bool isDetected)
    {
        if(OnPlatformDetected != null)
        {
            OnPlatformDetected(isDetected);
        }
    }

    public void InitializeMirrors(int nMirrors)
    {
        if(OnInitializeMirrors != null)
        {
            OnInitializeMirrors(nMirrors);
        }
    }

    public void ToggleLocked(int index, bool locked)
    {
        if(OnToggleLock != null)
        {
            OnToggleLock(index, locked);
        }
    }

    public void ResetMirrors()
    {
        if(OnResetMirrors != null)
        {
            OnResetMirrors();
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

    public void LevelChanged(Level newLevel)
    {
        if(OnLevelChanged != null)
        {
            OnLevelChanged(newLevel);
        }
    }
}
