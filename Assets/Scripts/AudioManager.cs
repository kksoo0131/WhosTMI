using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public AudioClip backGroundMusic1,
        backGroundMusic2,
        Filp,
        Success,
        Fail;

    AudioSource[] audioSources = new AudioSource[5];//MusicType ÀÇ Å©±â

    public enum MusicType
    {
        backGroundMusic1,
        backGroundMusic2,
        Filp,
        Success,
        Fail
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSources[(int)MusicType.backGroundMusic1].clip = backGroundMusic1;
        audioSources[(int)MusicType.backGroundMusic2].clip = backGroundMusic2;
        audioSources[(int)MusicType.Filp].clip = Filp;
        audioSources[(int)MusicType.Success].clip = Success;
        audioSources[(int)MusicType.Fail].clip = Fail;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool PlayMusic(MusicType musicType)
    {
        if (musicType == MusicType.Fail)
        {
            audioSources[(int)musicType].PlayOneShot(Fail);
            return true;
        }
        else if (musicType == MusicType.Success)
        {
            audioSources[(int)musicType].PlayOneShot(Success);
            return true;
        }
        else if (musicType == MusicType.Filp)
        {
            audioSources[(int)musicType].PlayOneShot(Filp);
            return true;
        }
        else
        {
            audioSources[(int)musicType].Play();
            return audioSources[(int)musicType].isPlaying;
        }
    }
    public bool CancelMusic(MusicType musicType)
    {
        audioSources[(int)musicType].Stop();

        return !audioSources[(int)musicType].isPlaying;
    }

    public bool PauseMusic(MusicType musicType)
    {
        audioSources[(int)musicType].Pause();

        return !audioSources[(int)musicType].isPlaying;
    }

    public bool UnPauseMusic(MusicType musicType)
    {
        audioSources[(int)musicType].UnPause();

        return audioSources[(int)musicType].isPlaying;
    }

}