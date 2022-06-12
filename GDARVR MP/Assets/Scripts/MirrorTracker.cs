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
                Debug.Log("CHECKINGGGGG");
                MirrorPlacer.Instance?.RayCastFromARCamera(TranslationTargetPosToScreenSpace(i), i);
                SetMirrorRotationAccordingToTarget(i);
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
        Debug.Log("TARG: " + target);
        for(int i = 0; i < mirrorTargets.Count; i++)
        {
            Debug.Log("TARG: " + GameObject.ReferenceEquals(mirrorTargets[i].gameObject, target));
            if(GameObject.ReferenceEquals(mirrorTargets[i].gameObject, target))
            {
                objFound = true;
                index = i;
                break;
            }
        }
        
        if(!objFound) return;
        Debug.Log("TARGEEEEEEEEEET");
        MirrorPlacer.Instance?.RayCastFromARCamera(TranslationTargetPosToScreenSpace(index), index);
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

    public void SetMirrorRotationAccordingToTarget(int i)
    {
        GameObject mirr = MirrorPlacer.Instance.GetMirror(i);
        mirr.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, -mirrorTargets[i].gameObject.transform.rotation.eulerAngles.y);
    }    
}
