using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlacer : MonoBehaviour
{
    private static MirrorPlacer instance;
    public static MirrorPlacer Instance { get{ return instance; } }
    private bool existing = false;

    private List<GameObject> mirrors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            existing = true;
            Destroy(gameObject);
        }
        else
        {
            foreach(Transform child in this.transform)
            {
                mirrors.Add(child.gameObject);
            }
            
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RayCastFromARCamera(Vector2 touchPos, int objIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("MirrorPlane")))
        {
            Debug.Log("HIT");
            Vector3 hitPoint = hit.point;
            // Spawn mirror here
            PlaceMirror(hitPoint, objIndex);
        }
    }

    public void PlaceMirror(Vector3 position, int objIndex)
    {
        if(objIndex < 0 || objIndex >= mirrors.Count) return;

        mirrors[objIndex].SetActive(true);
        mirrors[objIndex].transform.position = position;
    }

    public void DisableMirror(int index)
    {

    }

    private void OnDestroy()
    {
        if(!existing)
        {
            instance = null;
        }
    }
}
