using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehavior : MonoBehaviour
{
    [SerializeField] private float heatTimeGoal = 3f;
    private float heatedTime = 0f;
    
    public void HeatUp()
    {
        if(heatedTime >= heatTimeGoal) return;
        
        Debug.Log($"Heating: {heatedTime.ToString("F2")}");
        heatedTime += Time.deltaTime;

        if(heatedTime >= heatTimeGoal)
        {
            FullyCharged();
        }
    }

    private void FullyCharged()
    {
        // Play Animation here!
        Debug.Log("ANIMATION CHARGED PLAY");
        EventManager.Instance?.CrystalCharged();
    }
}
