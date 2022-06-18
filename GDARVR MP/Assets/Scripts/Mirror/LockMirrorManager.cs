using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockMirrorManager : MonoBehaviour
{
    private static LockMirrorManager instance;
    public static LockMirrorManager Instance { get{ return instance; } }

    private bool existing = false;

    [SerializeField] private GameObject lockPanel;
    [SerializeField] private GameObject buttonPrefab;

    private List<bool> lockList = new List<bool>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            existing = true;
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;

            EventManager.Instance.OnInitializeMirrors += InitializeButtons;
        }
    }

    // Only initialize from mirror tracker;
    private void InitializeButtons(int numMirrors)
    {
        for (int i = 0; i < numMirrors; i++)
        {
            int index = i;
            lockList.Add(false);
            GameObject newButtonObj = GameObject.Instantiate(buttonPrefab);
            newButtonObj.GetComponentInChildren<Text>().text = (i + 1).ToString();

            Button button = newButtonObj.GetComponent<Button>();
            
            button.onClick.AddListener(delegate{
                ToggleLock(button, index);
                });

            newButtonObj.transform.parent = lockPanel.transform;
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

    private void OnDestroy()
    {
        if(!existing)
        {
            EventManager.Instance.OnInitializeMirrors -= InitializeButtons;
        }
    }
}
