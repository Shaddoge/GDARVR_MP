using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBehavior : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private int maxReflect = 3;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool reflectOnlyMirror;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = maxReflect + 1;
        lr.SetPosition(0, startPoint.position);
    }

    void Update()
    {
        CastLaser(startPoint.position, startPoint.up);
    }

    private void CastLaser(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, startPoint.position);

        for (int i = 0; i < maxReflect; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300, 1))
            {
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                lr.SetPosition(i + 1, hit.point);
                
                if (!hit.transform.CompareTag("Mirror") && reflectOnlyMirror)
                {
                    for (int j = i + 1; j <= maxReflect; j++)
                    {
                        lr.SetPosition(j, hit.point);
                    }
                    break;
                }
            }
        }

    }
}
