using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public List<GameObject> LevelButtonList;

    public string levelSceneNameToLoad;
    public int levelNum;
    [SerializeField] float buttonAlpha = 0.5f;
    [SerializeField] int unlockedLevels = 1;



    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnUnlockLevel += UpdateAvailableLevels;
        
        if(GameManager.Instance != null)
        {
            unlockedLevels = GameManager.Instance.LevelCleared + 1;
        }
        else
        {
            unlockedLevels = 1;
        }

        EventManager.Instance?.UnlockLevels(unlockedLevels);

        Debug.Log("Levels Unlocked:" + unlockedLevels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectThisLevel()
    {
        // retrieves the level details of the selected level
        if (EventSystem.current.currentSelectedGameObject.CompareTag("LevelButton") == true)
        {
            LevelDetails levelDetails = EventSystem.current.currentSelectedGameObject.GetComponent <LevelDetails>();// get level details comp

            // updates the level scene name and level number to load
            levelSceneNameToLoad = levelDetails.level.SceneName; 
            levelNum = levelDetails.level.levelNumber;
        }
    }
    public void StartSelectedLevel()
    {
        // loads the selected level
        Debug.Log("Current Selected Level: " + levelSceneNameToLoad);

        SCENE_MANAGER.instance.LoadLevelByNum(levelNum);
        //EventManager.Instance?.LevelChanged(levelNum);
    }

    public void UpdateAvailableLevels(int nUnlockedLevels)
    {
        //Mathf.Clamp(nUnlockedLevels, 1.0f, LevelButtonList.Count);

        for (int i = 0; i < nUnlockedLevels; i++)
        {
            LevelButtonList[i].GetComponent<LevelDetails>().level.isLocked = false;
        }
        UpdateAvailableButtons();
    }
    public void UpdateAvailableButtons()
    {
        for (int i = 0; i < LevelButtonList.Count; i++)
        {
            LevelDetails levelDetails = LevelButtonList[i].GetComponent<LevelDetails>();
            Button button = LevelButtonList[i].GetComponentInChildren<Button>();
            Text text = LevelButtonList[i].GetComponentInChildren<Text>();
            Color origColor = text.color;



            if (levelDetails.level.isLocked == true)
            {
                Debug.Log(LevelButtonList[i].name + " locked");
                button.interactable = false;
                text.color = new Color(origColor.r, origColor.g, origColor.b, buttonAlpha);
                text.text = "Locked";
            }
            else if (levelDetails.level.isLocked == false)
            {
                Debug.Log(LevelButtonList[i].name + " unlocked");
                LevelButtonList[i].GetComponent<Button>().interactable = true;
                text.color = new Color(origColor.r, origColor.g, origColor.b, 1.0f);
                text.text = "LEVEL" +levelDetails.level.levelNumber;

            }
        }
    }

    private void OnDestroy()
    {
       EventManager.Instance.OnUnlockLevel -= UpdateAvailableLevels;
    }






}
