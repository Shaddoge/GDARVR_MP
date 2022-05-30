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
        int mirrorsHit = 0;
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
                    for (int j = i + 1; j <= maxReflect; j++)
                    {
                        laser.SetPosition(j, hit.point);
                        if(!hitParticle.activeSelf)
                            hitParticle.SetActive(true);
                    }
                    hitParticle.transform.LookAt(hit.normal);
                    hitParticle.transform.position = hit.point;
                    //hitParticle.SetActive(true);
                    break;
                }
                else
                {
                    mirrorsHit++;
                }
            }
            if(mirrorsHit == maxReflect)
            {
                hitParticle.transform.LookAt(hit.normal);
                hitParticle.transform.position = hit.point;
                //hitParticle.SetActive(true);
            }
        }

    }
}
