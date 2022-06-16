using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text timeTakenTxt;
    [SerializeField] private Text currentScoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LevelSelect()
    {
        EventManager.Instance?.LevelSelect();
    }

    public void NextLevel()
    {
        EventManager.Instance?.NextLevelClick();
    }

    public void SetTimeTaken(float _timeTaken)
    {
        if(timeTakenTxt != null)
            timeTakenTxt.text = "YOU TOOK " + _timeTaken.ToString() + " SECONDS TO COMPLETE THIS LEVEL";
    }

    public void SetCurrentScore(float _currentScore)
    {
        if (currentScoreTxt != null)
            currentScoreTxt.text = "YOU SCORED " + _currentScore.ToString() + "POINTS FOR THIS RUN";
    }
}
