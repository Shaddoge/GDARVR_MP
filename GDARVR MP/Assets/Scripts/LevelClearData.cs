using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearData
{
    public int time = 0;
    public int mirrorsPlaced = 0;
    public int score = 0;
    public int highScore = 0;

    public void CalculateScore(int timeFinished, int mirrorsPlaced)
    {
        //score = 1500 * Mathf.Clamp(180 - timeFinished, 0, 180);
        this.score = ((timeFinished / (timeFinished * timeFinished)) * 1500) + ((5 - mirrorsPlaced) * 5);
        
        if(this.highScore < this.score)
            this.highScore = this.score;
    }
}
