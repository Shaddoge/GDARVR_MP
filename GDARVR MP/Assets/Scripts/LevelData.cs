using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    

    private static Dictionary<int, levelData> levelDataList;

    public static void AddNewLevel(int i)
    {
        if(levelDataList != null)
        {
            if(!(levelDataList.ContainsKey(i)))
            {
                levelData newLevelData = new levelData(0.0f, 0.0f, 5);
                levelDataList.Add(i, newLevelData);
            }
            
        }

        else
        {
            levelDataList = new Dictionary<int, levelData>();
            levelData newLevelData = new levelData(0.0f, 0.0f, 5);
            levelDataList.Add(i, newLevelData);
        }
    }

    public static void SetLevelHighScore(int _level, float _highScore)
    {
        if(levelDataList.ContainsKey(_level))
            levelDataList[_level].UpdateHighScore(_highScore);
    }

    public static void SetLevelFastestTime(int _level, float _timeCleared)
    {
        if (levelDataList.ContainsKey(_level))
            levelDataList[_level].UpdateFastestTimeCleared(_timeCleared);
    }

    public static void SetLevelLeastMirrorsUsed(int _level, int _mirrorsUsed)
    {
        if (levelDataList.ContainsKey(_level))
            levelDataList[_level].UpdateLeastMirrorsUsed(_mirrorsUsed);
    }

    public static void UpdateLevelData(int _level, float _highScore, float _fastestTimeCleared, int _leastMirrorsUsed)
    {
        if (levelDataList.ContainsKey(_level))
            levelDataList[_level].UpdateLevelData(_highScore, _fastestTimeCleared, _leastMirrorsUsed);
    }

    public static float GetHighScore(int _level)
    {
        if (levelDataList.ContainsKey(_level))
        {
            return levelDataList[_level].GetHighScore();
        }

        else return -1.0f;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------//

    public struct levelData
    {

        float highScore;
        float fastestTimeCleared;
        int leastMirrorsUsed;


        public levelData(float _highScore, float _fastestTimeCleared, int _leastMirrorsUsed)
        {
            highScore = _highScore;
            fastestTimeCleared = _fastestTimeCleared;
            leastMirrorsUsed = _leastMirrorsUsed;
        }

        public void UpdateLevelData(float _highScore, float _fastestTimeCleared, int _leastMirrorsUsed)
        {
            highScore = _highScore;
            fastestTimeCleared = _fastestTimeCleared;
            leastMirrorsUsed = _leastMirrorsUsed;
        }

        public void UpdateHighScore(float _highScore)
        {
            highScore = _highScore;
        }

        public void UpdateFastestTimeCleared(float _fastestTimeCleared)
        {
            fastestTimeCleared = _fastestTimeCleared;
        }

        public void UpdateLeastMirrorsUsed(int _leastMirrorsUsed)
        {
            leastMirrorsUsed = _leastMirrorsUsed;
        }
        
        public float GetHighScore()
        {
            return highScore;
        }

        public float GetFastestTimeCleared()
        {
            return fastestTimeCleared;
        }

        public float GetLeastMirrorsUsed()
        {
            return leastMirrorsUsed;
        }

    }
}
