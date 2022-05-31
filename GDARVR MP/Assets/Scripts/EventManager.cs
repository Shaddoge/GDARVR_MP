using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if(instance == null)
                instance = GameObject.FindObjectOfType<EventManager>();
            return instance;
        }
    }
     
    public event Action<bool> OnPlatformTracked;
    public event Action<bool> OnMirrorTracked;

    public event Action OnCrystalCharged;
    public event Action OnRestart, OnGameOver;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
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

    public void GameOver()
    {
        if(OnGameOver != null)
        {
            OnGameOver();
        }
    }
}
