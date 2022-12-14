using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Perfect : MonoBehaviour
{
    public static int R_p_Count = 0;
    public static string R_p_state ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RHand")
        {
            Destroy(transform.parent.gameObject);
            NoteMaking.R_perfect++;
            R_p_Count++;
            R_p_state = "RPerfect";
            other.SendMessage("PlaySound", NoteSound.Sfx.Hit);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, NoteMaking.speed * 0.15f * 0.5f);
        GetComponent<CapsuleCollider>().height = NoteMaking.speed * 0.15f;//0.15가 150ms
    }

}
