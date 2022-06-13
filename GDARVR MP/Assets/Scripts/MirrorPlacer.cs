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
            Destroy(this.gameObject);
            return;
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
        //foreach(GameObject mirror in mirrors)
        //{
        //    Vector3 parentRotation = mirror.transform.parent.rotation.eulerAngles;
        //    mirror.transform.rotation = Quaternion.Euler(new Vector3())
        //}
    }

    public void RayCastFromARCamera(Vector2 touchPos, int objIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("MirrorPlane")))
        {
            Vector3 hitPoint = hit.point;
            // Spawn mirror here
            PlaceMirror(hitPoint, objIndex);
        }
    }

    public void PlaceMirror(Vector3 position, int objIndex)
    {
        if(objIndex < 0 || objIndex >= mirrors.Count) return;

        mirrors[objIndex].SetActive(true);
        Debug.Log($"Positions: {position.x}, {position.z}");
        Vector3 gridPos = new Vector3(Mathf.Round(position.x), this.transform.position.y, Mathf.Round(position.z));
        mirrors[objIndex].transform.position = gridPos;
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

    public GameObject GetMirror(int i)
    {
        return mirrors[i];
    }
}
