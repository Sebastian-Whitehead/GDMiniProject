using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectAudioManager : MonoBehaviour
{
    
    
    public AudioClip[] soundList = new AudioClip[2]; // Public list of audio clips to set in unity
    private AudioSource _soundSource;                // Audio Source Located on this game object
    
    void Start()
    {
        _soundSource = gameObject.GetComponent<AudioSource>();
    }

    // A series of overload function each to be called from other scripts
    public void PlaySoundFromObject(int index) // play sound with a given index from the public soundList
    {
        _soundSource.clip = soundList[index];
        _soundSource.Play();
    }

    public void PlaySoundFromObject(int index, float volume)    // play sound with a given index at a specified volume
    {
        _soundSource.PlayOneShot(soundList[index], volume);
    }

    public void PlaySoundFromObject(AudioClip soundClip)        // play a provided audio clip
    {
        _soundSource.clip = soundClip;
        _soundSource.Play();
    }

    public void PlaySoundFromObject(AudioClip soundClip, float volume) // play a provided audio clip at a specified volume
    {
        _soundSource.PlayOneShot(soundClip, volume);
    }
}
