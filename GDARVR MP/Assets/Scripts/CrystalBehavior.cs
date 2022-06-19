using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehavior : MonoBehaviour
{
    [SerializeField] private float chargeTimeGoal = 10f;
    private float chargedTime = 0f;
    private float timeNotCharged;
    private bool charging;
    private Animator animator;

    [Header("Debug controls")]
    [SerializeField] bool AutoChargeCrystal = false;

    [Header("Particles")]
    [SerializeField] private ParticleSystem chargeParticle;
    [SerializeField] private ParticleSystem burstParticle;
    [SerializeField] private ParticleSystem shockwave;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        // debug
        if (AutoChargeCrystal == true)
        {
            HeatUp();
        }

        if (chargeTimeGoal < chargedTime) return;
        if (charging)
            timeNotCharged += Time.deltaTime;

        if (animator.GetBool("Charging") && timeNotCharged >= 0.25f)
        {
            animator.SetBool("Charging", false);
            // play not discharge
            AudioManager.Instance.PlayDesSFX();
            chargeParticle.Stop();
            timeNotCharged = 0f;
            charging = false;
        }
    }

    public void HeatUp()
    {
        if (chargedTime >= chargeTimeGoal) return;
        
        if (!animator.GetBool("Charging"))
        {
            animator.SetBool("Charging", true);
            // play crystal build up sfx
            AudioManager.Instance.PlayChargingSFX();
            Debug.Log("Should play build up sfx");
        }

        if (!charging)
        {
            charging = true;
            Debug.Log("Play!");
            chargeParticle.Play();
        }
            
        Debug.Log($"Heating: {chargedTime.ToString("F2")}");
        chargedTime += Time.deltaTime;

        if (MenuHUD.Instance)
        {
            int chargePercent = (int)((chargedTime / chargeTimeGoal) * 100);
            MenuHUD.Instance.UpdateChargePercent(chargePercent);
        }

        timeNotCharged = 0;

        if (chargedTime >= chargeTimeGoal)
        {
            FullyCharged();
        }
    }

    private void FullyCharged()
    {
        // Play Animation here!
        
        burstParticle.Play();
        shockwave.Play();
        animator.SetTrigger("FullCharged");
        // Insert sfx play
        AudioManager.Instance.PlayChargedSFX();
        EventManager.Instance?.CrystalCharged();
    }
}
