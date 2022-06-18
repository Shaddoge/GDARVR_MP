using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockMirrorManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    private int numMirrors = 0;
    private List<bool> lockList = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject mirrorPlane = GameObject.FindObjectOfType<MirrorPlacer>().gameObject;
        numMirrors = mirrorPlane.transform.childCount;

        for (int i = 0; i < numMirrors; i++)
        {
            int index = i;
            lockList.Add(false);
            GameObject newButtonObj = GameObject.Instantiate(buttonPrefab, this.transform);
            newButtonObj.GetComponentInChildren<Text>().text = (i + 1).ToString();

            Button button = newButtonObj.GetComponent<Button>();
            
            button.onClick.AddListener(delegate{
                ToggleLock(button, index);
                });
        }
    }

    private void ToggleLock(Button button, int index)
    {
        AudioManager.Instance.PlayLockSFX();
        Debug.Log(index);

        lockList[index] = !lockList[index];
        bool locked = lockList[index]; 

        Debug.Log(lockList[index]);
        if(locked)
            button.image.color = new Color32(0, 150, 180, 175);
        else
            button.image.color = new Color32(0, 0, 0, 175);

        EventManager.Instance?.ToggleLocked(index, locked);
    }
}
