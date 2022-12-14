using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeIcon : MonoBehaviour
{
    public GameObject Volume1,Volume2,Volume3,Mute;
    bool v3,v2,v1,mu = true;
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);
        // Volume2.gameObject.SetActive(false);
        // Volume3.gameObject.SetActive(false);
        // Mute.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        Volume1.gameObject.SetActive(v3);
        Volume2.gameObject.SetActive(v2);
        Volume3.gameObject.SetActive(v1);
        Mute.gameObject.SetActive(mu);

        if(SoundManager.Volumes <0.04f)
        {
            v3 = false;
            v2 = false;
            v1 = false;
            mu = true;
        }
        else if(SoundManager.Volumes >0.04f && SoundManager.Volumes <0.33f)
        {
            v3 = false;
            v2 = false;
            v1 = true;
            mu = false;
        }
        else if(SoundManager.Volumes >0.33f && SoundManager.Volumes <0.63f)
        {
            v3 = false;
            v2 = true;
            v1 = false;
            mu = false;
        }
        else if(SoundManager.Volumes >0.63f && SoundManager.Volumes <=1)
        {
            v3 = true;
            v2 = false;
            v1 = false;
            mu = false;
        }

    }
}
