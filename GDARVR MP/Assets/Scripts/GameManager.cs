using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
                instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnCrystalCharged += CrystalCharged;
    }

    private void CrystalCharged()
    {
        StartCoroutine(GameOverDelay(2));
    }

    private IEnumerator GameOverDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameOver();
    }

    private void GameOver()
    {
        // Should call UI Manager to popup game over screen
        EventManager.Instance?.GameOver();
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnCrystalCharged -= CrystalCharged;
    }
}
