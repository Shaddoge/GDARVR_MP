using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private int maxReflect = 3;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool reflectOnlyMirror;
    
    private int mirrorsHit = 0;
    private int playerMirrorsHit = 0;
    private int prevPlayerMirrorsHit = 0;

    void Start()
    {
        laser.positionCount = maxReflect + 1;
        laser.SetPosition(0, startPoint.position);
        laser.enabled = true;
        hitParticle.SetActive(false);
    }

    void Update()
    {
        CastLaser(startPoint.position, startPoint.up);
    }

    private void CastLaser(Vector3 position, Vector3 direction)
    {
        mirrorsHit = 0;
        playerMirrorsHit = 0;
        laser.SetPosition(0, startPoint.position);

        //hitParticle.SetActive(false);
        for (int i = 0; i < maxReflect; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300, 1))
            {
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                laser.SetPosition(i + 1, hit.point);

                if (!hit.transform.CompareTag("Mirror") && reflectOnlyMirror)
                {
                    if (hit.transform.CompareTag("Crystal"))
                    {
                    CrystalBehavior crystal = hit.transform.GetComponent<CrystalBehavior>();
                    crystal.HeatUp();
                    }

                    for (int j = i + 1; j <= maxReflect; j++)
                    {
                        laser.SetPosition(j, hit.point);
                        if(!hitParticle.activeSelf)
                            hitParticle.SetActive(true);
                    }
                    hitParticle.transform.LookAt(hit.normal);
                    hitParticle.transform.position = hit.point;
                    break;
                }
                else
                {
                    mirrorsHit++;
                    // If mirror is from player
                    MirrorPlacer mirrorPlacer = hit.transform.parent.GetComponentInParent(typeof(MirrorPlacer)) as MirrorPlacer;
                    if(mirrorPlacer != null)
                    {
                        playerMirrorsHit++;
                    }
                }
            }
            if(mirrorsHit == maxReflect)
            {
                hitParticle.transform.LookAt(hit.normal);
                hitParticle.transform.position = hit.point;
            }
        }
        if(prevPlayerMirrorsHit != playerMirrorsHit)
        {
            OnPlayerMirrorHitNumChanged();
        }
    }

    private void OnPlayerMirrorHitNumChanged()
    {
        prevPlayerMirrorsHit = playerMirrorsHit; // number of mirror hit before update
        GameManager.Instance?.UpdateMirrorsUsed(playerMirrorsHit);
        MenuHUD.Instance?.UpdateMirrorsUsed(playerMirrorsHit);
    }
}
