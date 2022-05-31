using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlatformTracker : MonoBehaviour
{
    private ObserverBehaviour platformTarget;

    // Start is called before the first frame update
    void Start()
    {
        platformTarget = GameObject.FindGameObjectWithTag("PlatformTarget").GetComponent<ObserverBehaviour>();
        if(platformTarget != null)
        {
            platformTarget.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        else
        {
            Debug.LogError("Platform Target not found!");
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour target, TargetStatus targetStatus)
    {
        if(targetStatus.Status == Status.NO_POSE)
        {
            OnTargetLost();
        }
        else
        {
            OnTargetDetected();
        }
    }

    public void OnTargetDetected()
    {
        EventManager.Instance?.PlatformTracked(true);
    }

    public void OnTargetLost()
    {
        EventManager.Instance?.PlatformTracked(false);
    }
}
