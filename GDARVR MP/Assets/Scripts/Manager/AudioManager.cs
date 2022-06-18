using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    private bool existing = false;


    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource uiSfxSource;
    [SerializeField] private AudioSource gameSFXSource;

    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip buttonSFX;
    [SerializeField] private AudioClip mirrorSFX;


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            existing = true;
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (!existing)
        {
            Debug.Log("Audio Manager has been destroyed");
        }
    }

    public void PlayMainBGM()
    {
        musicSource.PlayOneShot(gameBGM);
    }

    public void PlaySound(AudioClip clip)
    {
        //uisfxSource.PlayOneShot(clip);
    }

    public void PlayButtonSFX()
    {
        uiSfxSource.PlayOneShot(buttonSFX);
        Debug.Log("play button sfx");

    }

    public void PlayMirrorSFX()
    {
        gameSFXSource.PlayOneShot(mirrorSFX);
        Debug.Log("play mirror sfx");

    }

}
