using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].volume = 0.1f;

        }
        audioSources[(int)MusicType.backGroundMusic1].clip = backGroundMusic1;
        audioSources[(int)MusicType.backGroundMusic2].clip = backGroundMusic2;
        audioSources[(int)MusicType.backGroundMusic2].pitch = 1.5f;
        audioSources[(int)MusicType.Flip].clip = Flip;
        audioSources[(int)MusicType.Success].clip = Success;
        audioSources[(int)MusicType.Fail].clip = Fail;
    }

    public AudioClip backGroundMusic1,
        backGroundMusic2,
        Flip,
        Success,
        Fail;

    public AudioSource[] audioSources = new AudioSource[5];//MusicType ÀÇ Å©±â

    public enum MusicType
    {
        backGroundMusic1,
        backGroundMusic2,
        Flip,
        Success,
        Fail
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
        else if (musicType == MusicType.Flip)
        {
            audioSources[(int)musicType].PlayOneShot(Flip);
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