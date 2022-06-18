using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlacer : MonoBehaviour
{
    private static MirrorPlacer instance;
    public static MirrorPlacer Instance { get{ return instance; } }
    private bool existing = false;

    [SerializeField] private GameObject mirrorPrefab;
    private List<GameObject> mirrors = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            existing = true;
            Destroy(this.gameObject);
            return;
        }
        else
        {
            /*if (this.transform.childCount > 0)
            {
                foreach(Transform child in this.transform)
                {
                    mirrors.Add(child.gameObject);
                }
            }*/
            
            EventManager.Instance.OnToggleLock += LockMirror;
            EventManager.Instance.OnResetMirrors += ResetMirrors;
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

    public void RayCastFromARCamera(Vector2 touchPos, float rotY, int objIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("MirrorPlane")))
        {
            Vector3 hitPoint = hit.point;
            // Spawn mirror here
            PlaceMirror(hitPoint, rotY, objIndex);
        }
    }

    public void PlaceMirror(Vector3 position, float rotY, int objIndex)
    {
        if (objIndex < 0 || objIndex >= mirrors.Count) return;
        // Return if locked
        if (mirrors[objIndex].GetComponent<Mirror>().isLocked) return;

        mirrors[objIndex].SetActive(true);
        
        Vector3 gridPos = new Vector3(Mathf.Round(position.x), this.transform.position.y, Mathf.Round(position.z));
        mirrors[objIndex].transform.position = gridPos;
        mirrors[objIndex].transform.localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
    }

    public void AddMirrors(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newMirror = GameObject.Instantiate(mirrorPrefab);
            mirrors.Add(newMirror);
            newMirror.transform.parent = this.transform;
        }
    }

    private void LockMirror(int index, bool locked)
    {
        if (mirrors[index] != null)
            mirrors[index].GetComponent<Mirror>().isLocked = locked;
        else
            Debug.Log("Mirror object does not exist!");
    }

    private void ResetMirrors()
    {
        foreach(GameObject mirror in mirrors)
        {
            mirror.SetActive(false);
            mirror.transform.position = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        if (!existing)
        {
            instance = null;
        }
    }
}
