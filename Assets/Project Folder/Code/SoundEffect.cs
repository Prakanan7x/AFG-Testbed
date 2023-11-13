using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

    public float masterVolume = 1.0f;
    public AudioSource[] VoiceLine = new AudioSource[25];
    public AudioSource[] Sound_Effect = new AudioSource[25];

    public AudioSource BlockHit1;
    public AudioSource[] Hit = new AudioSource[10];



    private void Start()
    {
        /*
        for(int i = 0; i < AttackReady.Length; i++)
        {
            AttackReady[i].volume = masterVolume;
        }
        for (int i = 0; i < AttackSwing.Length; i++)
        {
            AttackReady[i].volume = masterVolume;
        }
        */

    }

    // Use this for initialization
    public void PlayVoiceLine(int code)
    {
        VoiceLine[code].volume = masterVolume;
        VoiceLine[code].Play();
    }
    public void PlaySound_Effect(int code)
    {
        Sound_Effect[code].volume = masterVolume;
        Sound_Effect[code].Play();

    }

    public void PlayBlockHit1()
    {
        BlockHit1.volume = masterVolume;
        BlockHit1.Play();
    }
    public void PlayHit(int code)
    {
        //print("PlayHIT===========" + code);
        Hit[code].volume = masterVolume;
        Hit[code].Play();
    }
}
