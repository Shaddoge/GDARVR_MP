using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayManager : MonoBehaviour
{
    public static DisplayManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] Image bg;
    [SerializeField] Image preview;
    [SerializeField] Text levelName;
    [SerializeField] Text difficulty;
    [SerializeField] Text highscore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay()
    {

        if (EventSystem.current.currentSelectedGameObject.CompareTag("LevelButton") == true)
        {
            LevelDetails levelDetails = EventSystem.current.currentSelectedGameObject.GetComponent<LevelDetails>();// get level details comp

            // update display

            bg.sprite = levelDetails.level.levelPreview;
            preview.sprite = levelDetails.level.levelPreview;

            levelName.text = levelDetails.level.levelName;
            difficulty.text = (levelDetails.level.difficulty); // temp
            highscore.text = levelDetails.level.highscore.ToString();
        }
    }
}
