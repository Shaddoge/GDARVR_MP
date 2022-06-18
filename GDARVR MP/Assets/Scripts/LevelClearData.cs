using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearData
{
    public int time = 0;
    public int mirrorsUsed = 0;
    public int score = 0;
    public int highScore = 0;

    public void CalculateScore(int _timeFinished, int _mirrorsUsed)
    {
        //score = 1500 * Mathf.Clamp(180 - timeFinished, 0, 180);
        Debug.Log(Mathf.Clamp((int)(_timeFinished / 12), 0, 10) * 100);
        Debug.Log((5 - (int)Mathf.Clamp(_mirrorsUsed, 0, 5)) * 50);
        this.score = (1000 - Mathf.Clamp((int)(_timeFinished / 12), 0, 10) * 100) + ((5 - (int)Mathf.Clamp(_mirrorsUsed, 0, 5)) * 50);
        
        if(this.highScore < this.score)
            this.highScore = this.score;
    }
}
