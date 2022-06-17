using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanelObj;
    [SerializeField] private GameObject[] hud;

    void Start()
    {
        gameOverPanelObj.SetActive(false);
        EventManager.Instance.OnGameClear += GameOverPopUp;
    }

    private void GameOverPopUp(LevelClearData levelClearData)
    {
        // Disable HUD
        for (int i = 0; i < hud.Length; i++)
        {
            hud[i].SetActive(false);
        }
        // Display Game Over Panel
        GameOverPanel gameOverPanel = gameOverPanelObj.GetComponent<GameOverPanel>();

        gameOverPanel.LevelCleared(levelClearData);
        gameOverPanelObj.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnGameClear -= GameOverPopUp;
    }
}
