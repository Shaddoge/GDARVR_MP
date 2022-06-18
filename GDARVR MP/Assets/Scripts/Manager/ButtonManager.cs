using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public List<GameObject> LevelButtonList;

    public Level levelToLoad;
    [SerializeField] private Button startButton;
    [SerializeField] private float buttonAlpha = 0.5f;
    [SerializeField] private int unlockedLevels = 1;

    // Start is called before the first frame update
    void Start()
    {
        //EventManager.Instance.OnUnlockLevel += UpdateAvailableLevels;
        
        /*if(GameManager.Instance != null)
        {
            unlockedLevels = GameManager.Instance.LevelCleared + 1;
        }
        else
        {
            unlockedLevels = 1;
        }*/

        //EventManager.Instance?.UnlockLevels(unlockedLevels);
        UpdateAvailableButtons();
        Debug.Log("Levels Unlocked:" + unlockedLevels);
    }

    public void SelectThisLevel()
    {
        // retrieves the level details of the selected level
        if (EventSystem.current.currentSelectedGameObject.CompareTag("LevelButton") == true)
        {
            if(!startButton.IsInteractable())
            {
                startButton.interactable = true;
            }
            AudioManager.Instance.PlayLvlSelectSFX();
            LevelDetails levelDetails = EventSystem.current.currentSelectedGameObject.GetComponent <LevelDetails>();// get level details comp

            // updates the level scene name and level number to load
            levelToLoad = levelDetails.level; 
        }
    }
    public void StartSelectedLevel()
    {
        // loads the selected level
        Debug.Log("Current Selected Level: " + levelToLoad.SceneName);

        AudioManager.Instance.PlayButtonSFX();
        SCENE_MANAGER.Instance?.LoadLevel(levelToLoad);
        //SCENE_MANAGER.Instance?.LoadLevelByNum(levelNum);
        
    }

    /*public void UpdateAvailableLevels(int nUnlockedLevels)
    {
        //Mathf.Clamp(nUnlockedLevels, 1.0f, LevelButtonList.Count);

        for (int i = 0; i < nUnlockedLevels; i++)
        {
            LevelButtonList[i].GetComponent<LevelDetails>().level.isLocked = false;
        }
        UpdateAvailableButtons();
    }*/
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

    public void BackToMainMenu()
    {
        SCENE_MANAGER.Instance?.OpenMainMenu();
    }

    private void OnDestroy()
    {
       //EventManager.Instance.OnUnlockLevel -= UpdateAvailableLevels;
    }
}
