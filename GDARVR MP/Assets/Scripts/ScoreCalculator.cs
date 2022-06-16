using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private const float timeTakenVar = 1500.0f;
    private const float mirrorsUsedVar = 5.0f;
    public static float CalculateScore(float _timeTaken, int _mirrorsUsed)
    {
        return (((_timeTaken) / (_timeTaken * _timeTaken)) * timeTakenVar) + ((5 - _mirrorsUsed) * mirrorsUsedVar);
    }
}
