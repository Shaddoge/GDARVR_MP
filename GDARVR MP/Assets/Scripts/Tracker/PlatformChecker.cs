using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlatformChecker : MonoBehaviour
{
    private ObserverBehaviour platformTarget;

    // Start is called before the first frame update

    public void OnTargetDetected()
    {
        if(EventManager.Instance != null)
            EventManager.Instance.PlatformDetected(true);
    }

    public void OnTargetLost()
    {
        if(EventManager.Instance != null)
            EventManager.Instance.PlatformDetected(false);
    }
}
