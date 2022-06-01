using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        EventManager.Instance.OnGameClear += GameOverPopUp;
    }

    private void GameOverPopUp()
    {
        // Should enable game over screen
        Debug.Log("POP UP GAME OVER");
        gameOverPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnGameClear -= GameOverPopUp;
    }
}
