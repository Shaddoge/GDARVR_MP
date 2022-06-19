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
    private List<Button> buttonList = new List<Button>(); 

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
            instance = this;

            EventManager.Instance.OnInitializeMirrors += InitializeButtons;
            EventManager.Instance.OnResetMirrors += ResetLocks;
        }
    }

    // Only initialize from mirror tracker;
    private void InitializeButtons(int numMirrors)
    {
        for (int i = 0; i < numMirrors; i++)
        {
            int index = i;
            
            GameObject newButtonObj = GameObject.Instantiate(buttonPrefab);
            newButtonObj.GetComponentInChildren<Text>().text = (i + 1).ToString();

            Button button = newButtonObj.GetComponent<Button>();
            
            button.onClick.AddListener(delegate{
                ToggleLock(button, index);
                });

            lockList.Add(false);
            buttonList.Add(button);

            newButtonObj.transform.SetParent(lockPanel.transform);
        }
    }

    private void ToggleLock(Button button, int index)
    {
        AudioManager.Instance.PlayLockSFX();
        Debug.Log(index);

        lockList[index] = !lockList[index];

        if(lockList[index])
            button.image.color = new Color32(0, 150, 180, 175);
        else
            button.image.color = new Color32(0, 0, 0, 175);

        EventManager.Instance?.ToggleLocked(index, lockList[index]);
    }

    private void ResetLocks()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            lockList[i] = false;

            if(lockList[i])
                buttonList[i].image.color = new Color32(0, 150, 180, 175);
            else
                buttonList[i].image.color = new Color32(0, 0, 0, 175);

            EventManager.Instance?.ToggleLocked(i, false);
        }
    }

    private void OnDestroy()
    {
        if(!existing)
        {
            EventManager.Instance.OnInitializeMirrors -= InitializeButtons;
            EventManager.Instance.OnResetMirrors -= ResetLocks;
        }
    }
}
