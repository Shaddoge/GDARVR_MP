using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    private bool existing = false;


    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource uiSFXSource;
    [SerializeField] private AudioSource gameSFXSource;

    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip buttonSFX;
    [SerializeField] private AudioClip buttonSFX2;
    [SerializeField] private AudioClip lockSFX;




    [SerializeField] private AudioClip mirrorSFX;
    [SerializeField] private AudioClip crysAscSFX;
    [SerializeField] private AudioClip crysDesSFX;
    [SerializeField] private AudioClip crysFullSFX;



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

        AudioManager.instance.PlayMainBGM();
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

    public void PlayLvlSelectSFX()
    {
        Debug.Log("Play level sfx");
        uiSFXSource.PlayOneShot(buttonSFX2);
    }

    public void PlayButtonSFX()
    {
        uiSFXSource.PlayOneShot(buttonSFX);
        Debug.Log("play button sfx");

    }

    public void PlayMirrorSFX()
    {
        gameSFXSource.PlayOneShot(mirrorSFX);
        Debug.Log("play mirror sfx");

    }
    public void PlayChargingSFX()
    {
        CutSFX();
        gameSFXSource.PlayOneShot(crysAscSFX);
        Debug.Log("play charge sfx");

    }
    public void PlayDesSFX()
    {
        CutSFX();
        gameSFXSource.PlayOneShot(crysDesSFX);
        Debug.Log("play descend sfx");

    }
    public void PlayChargedSFX()
    {
        CutSFX();
        gameSFXSource.PlayOneShot(crysFullSFX);
        Debug.Log("play fully charged sfx");

    }

    public void PlayLockSFX()
    {
        uiSFXSource.PlayOneShot(lockSFX);
    }

    public void CutSFX()
    {
        if (gameSFXSource.isPlaying)
        {
            gameSFXSource.Stop();
        }
    }

}
