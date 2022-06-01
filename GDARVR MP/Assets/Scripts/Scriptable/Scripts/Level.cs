using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public string levelName;
    public string SceneName;
    public Sprite levelPreview;
    public int levelNumber;
    public bool isLocked = true;
    // Start is called before the first frame update
 
}
