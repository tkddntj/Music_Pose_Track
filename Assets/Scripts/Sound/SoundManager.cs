using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource musicsource;
    float time = 0f;
    string pose;
    public static float Volumes ;

    private void Start() 
    {
        musicsource = GetComponent<AudioSource>();
        musicsource.volume = 1f;
        Volumes = musicsource.volume;
    }

    private void Update() {
        if (SMPL_Move.pose_info == "RIGHT")
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                musicsource.volume += 0.05f;
                Volumes = musicsource.volume;
                time = 0f;
            } 
        }
        else if (SMPL_Move.pose_info == "LEFT")
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                musicsource.volume -= 0.05f;
                Volumes = musicsource.volume;
                time = 0f;
            } 
        }
        else
        {
            time = 0f;
        }
        
    }
}
