using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnGameOver += GameOverPopUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameOverPopUp()
    {
        // Should enable game over screen
        Debug.Log("POP UP GAME OVER");
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnGameOver -= GameOverPopUp;
    }
}
