using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MirrorTracker : MonoBehaviour
{
    [SerializeField] private List<ObserverBehaviour> mirrorTargets = new List<ObserverBehaviour>();
    [SerializeField] private List<bool> isTracked = new List<bool>();

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
                isTracked.Add(false);
                mirrorTarget.OnTargetStatusChanged += OnTargetStatusChanged;
            }
        }
        else
        {
            Debug.LogError("Mirror Target/s not found!");
        }
    }

    private void Update()
    {
        for(int i = 0; i < mirrorTargets.Count; i++)
        {
            if(isTracked[i])
            {
                Vector3 pos = TranslationTargetPosToScreenSpace(i);
                float rotY = Mathf.Round(mirrorTargets[i].gameObject.transform.rotation.eulerAngles.y / 30) * 30;
                MirrorPlacer.Instance?.RayCastFromARCamera(pos, rotY, i);
                //SetMirrorRotationAccordingToTarget(i);
            }
        }
        
    }

    void OnTargetStatusChanged(ObserverBehaviour target, TargetStatus targetStatus)
    {
        Debug.Log(target.gameObject);

        if(targetStatus.Status == Status.TRACKED)
        {
            OnTargetDetected(target.gameObject);
        }
        else
        {
            OnTargetLost(target.gameObject);
        }
    }

    public void OnTargetDetected(GameObject target)
    {
        int index = 0;
        bool objFound = false;

        for(int i = 0; i < mirrorTargets.Count; i++)
        {
            if(GameObject.ReferenceEquals(mirrorTargets[i].gameObject, target))
            {
                objFound = true;
                index = i;
                break;
            }
        }
        
        if(!objFound) return;
        
        Vector3 pos = TranslationTargetPosToScreenSpace(index);
        float rotY = Mathf.Round(mirrorTargets[index].gameObject.transform.rotation.eulerAngles.y / 30) * 30;
        MirrorPlacer.Instance?.RayCastFromARCamera(pos, rotY, index);
        isTracked[index] = true;
    }

    public void OnTargetLost(GameObject target)
    {
        int index = 0;
        bool objFound = false;

        for(int i = 0; i < mirrorTargets.Count; i++)
        {
            if(mirrorTargets[i] == target)
            {
                objFound = true;
                index = i;
                break;
            }
        }

        if(!objFound) return;

        isTracked[index] = false;
    }

    private Vector2 TranslationTargetPosToScreenSpace(int index)
    {
        return Camera.main.WorldToScreenPoint(mirrorTargets[index].transform.position);
    }  
}
