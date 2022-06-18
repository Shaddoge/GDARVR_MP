using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehavior : MonoBehaviour
{
    [SerializeField] private float heatTimeGoal = 10f;
    private float heatedTime = 0f;
    private float timeNotCharged;
    private bool charging;
    private Animator animator;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if(charging)
            timeNotCharged += Time.deltaTime;

        if(animator.GetBool("Charging") && timeNotCharged >= 1.0f)
        {
            animator.SetBool("Charging", false);
            // play not discharge
            AudioManager.Instance.PlayDesSFX();
            timeNotCharged = 0f;
        }
    }

    public void HeatUp()
    {
        if(heatedTime >= heatTimeGoal) return;
        
        if(!animator.GetBool("Charging"))
        {
            animator.SetBool("Charging", true);
            // play crystal build up sfx
            AudioManager.Instance.PlayChargingSFX();
            Debug.Log("Should play build up sfx");
        }

        if (!charging)
            charging = true;

        Debug.Log($"Heating: {heatedTime.ToString("F2")}");
        heatedTime += Time.deltaTime;
        timeNotCharged = 0;
        if(heatedTime >= heatTimeGoal)
        {
            FullyCharged();
        }
    }

    private void FullyCharged()
    {
        // Play Animation here!
        Debug.Log("ANIMATION CHARGED PLAY");
        animator.SetTrigger("FullCharged");
        //insert sfx play
        AudioManager.Instance.PlayChargedSFX();
        EventManager.Instance?.CrystalCharged();
    }
}
