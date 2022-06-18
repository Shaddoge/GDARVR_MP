using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text timeTakenTxt;
    [SerializeField] private Text mirrorsPlacedTxt;
    [SerializeField] private Text currentScoreTxt;
    [SerializeField] private Text highScoreTxt;

    public void LevelSelect()
    {
        SCENE_MANAGER.Instance?.OpenLevelSelect();
    }

    public void NextLevel()
    {
        SCENE_MANAGER.Instance?.LoadNextLevel();
    }

    private void SetTimeTaken(int _timeTaken)
    {
        if(timeTakenTxt != null)
            timeTakenTxt.text = $"Time: {_timeTaken}";
    }

    private void SetMirrorsPlaced(int _mirrorsPlaced)
    {
        if(mirrorsPlacedTxt != null)
            mirrorsPlacedTxt.text = $"Mirrors Placed: {_mirrorsPlaced}";
    }

    private void SetCurrentScore(int _currentScore)
    {
        if (currentScoreTxt != null)
            currentScoreTxt.text = $"Score: {_currentScore}";
    }

    private void SetHighScore(int _highScore)
    {
        if (highScoreTxt != null)
            highScoreTxt.text = $"High Score: {_highScore}";
    }

    public void LevelCleared(LevelClearData levelClearData)
    {
        SetTimeTaken(levelClearData.time);
        SetMirrorsPlaced(levelClearData.mirrorsUsed);
        SetCurrentScore(levelClearData.score);
        SetHighScore(levelClearData.highScore);
    }
}
