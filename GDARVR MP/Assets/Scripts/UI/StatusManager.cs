using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] private Text Status;
    // Start is called before the first frame update
    void Start()
    {
        
        EventManager.Instance.OnPlatformDetected += ToggleStatus;
    }

    private void ToggleStatus(bool isDetected)
    {
        Status.enabled = !isDetected;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnPlatformDetected -= ToggleStatus;
    }
}
