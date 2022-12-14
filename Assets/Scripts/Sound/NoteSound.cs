using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSound : MonoBehaviour
{
    public enum Sfx {Hit}  
    public AudioClip[] clips;   
    AudioSource audios; 

    void Awake()
    {
        audios = GetComponent<AudioSource>();   
    }

    void PlaySound(Sfx sfx)
    {
        audios.clip = clips[(int)sfx];  
        audios.Play();  
    }
}

    