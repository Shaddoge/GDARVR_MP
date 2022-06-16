using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject[] hud;

    void Start()
    {
        gameOverPanel.SetActive(false);
        EventManager.Instance.OnGameClear += GameOverPopUp;
    }

    private void GameOverPopUp()
    {
        // Disable HUD
        for (int i = 0; i < hud.Length; i++)
        {
            hud[i].SetActive(false);
        }
        // Display Game Over Panel
        gameOverPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnGameClear -= GameOverPopUp;
    }
}
