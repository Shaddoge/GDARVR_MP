using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MenuHUD : MonoBehaviour
{
    private static MenuHUD instance;
    public static MenuHUD Instance { get{ return instance; } }

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Text timeText;
    [SerializeField] private Text chargeText;
    [SerializeField] private Text mirrorUsedText;
    [SerializeField] private UnityEngine.UI.Image chargeBar;
    

    private void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            menuPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    public void UpdateTime(float time)
    {
        timeText.text = $"Time: {Mathf.RoundToInt(time)}";
    }

    public void UpdateChargePercent(int percent)
    {
        chargeBar.fillAmount = percent / 100f;
        chargeText.text = $"{percent}%";
    }

    public void UpdateMirrorsUsed(int num)
    {
        mirrorUsedText.text = $"Mirror/s used: {num}";
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public void FocusCamera()
    {
        // Start coroutine
        StartCoroutine(ChangeFocusMode());
    }

    private IEnumerator ChangeFocusMode()
    {
        // Set focus to normal mode
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_NORMAL);

        // Add delay
        yield return new WaitForSeconds(2);

        //Set focus to continuous auto mode
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    public void ResetMirrors()
    {
        EventManager.Instance?.ResetMirrors();
    }

    public void LevelSelect()
    {
        SCENE_MANAGER.Instance?.OpenLevelSelect();
    }
}
