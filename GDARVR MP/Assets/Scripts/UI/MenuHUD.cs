using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHUD : MonoBehaviour
{
    private static MenuHUD instance;
    public static MenuHUD Instance { get{ return instance; } }

    private void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }


    [SerializeField] private Text timeText;
    // Update is called once per frame
    public void UpdateTime(float time)
    {
        timeText.text = $"Time: {Mathf.RoundToInt(time)}";
    }
}
