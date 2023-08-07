using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum MusicType
    { 
        backGroundMusic1,
        backGroundMusic2,
        Success,
        Fail,
    }

    public bool PlayMusic(int _index) { return true; }
    public bool CancelMusic() { return true; }

    public bool PauseMusic() { return true; }

    public bool UnPauseMusic() { return true; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
