using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LevelMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject levelMenuPanel;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void LevelMenuPopUp()
    {
        // Pause game
        Time.timeScale = 0;

        // Display Level Menu Panel
        levelMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        // Resume game
        Time.timeScale = 1;

        // Close Level Menu Panel
        levelMenuPanel.SetActive(false);
    }

    public void FocusCamera()
    {
        // Resume game
        ResumeGame();

        // Start coroutine
        StartCoroutine(ChangeFocusMode());
    }

    IEnumerator ChangeFocusMode()
    {
        // Set focus to normal mode
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_NORMAL);

        // Add delay
        yield return new WaitForSeconds(2);

        //Set focus to continuous auto mode
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    public void OpenLevelSelect()
    {
        SCENE_MANAGER.Instance?.OpenLevelSelect();
    }

    public void OpenMainMenu()
    {
        SCENE_MANAGER.Instance?.OpenMainMenu();
    }
}
