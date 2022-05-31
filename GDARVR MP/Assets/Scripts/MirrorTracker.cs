using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MirrorTracker : MonoBehaviour
{
    private List<ObserverBehaviour> mirrorTargets = new List<ObserverBehaviour>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("MirrorTarget");

        if(targetObjects.Length > 0)
        {
            for (int i = 0; i < targetObjects.Length; i++)
            {
                ObserverBehaviour mirrorTarget = targetObjects[i].GetComponent<ObserverBehaviour>();
                mirrorTargets.Add(mirrorTarget);
                //mirrorTarget.OnTargetStatusChanged += OnTargetStatusChanged;;
            }
        }
        else
        {
            Debug.LogError("Mirror Target/s not found!");
        }
    }

     void OnTargetStatusChanged(ObserverBehaviour target, TargetStatus targetStatus)
    {
        
    }

    public void OnTargetDetected()
    {
        
    }

    public void OnTargetLost()
    {
        
    }
}
