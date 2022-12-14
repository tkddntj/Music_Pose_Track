using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Perfect : MonoBehaviour
{
    public static int L_p_Count = 0;
    public static string L_p_state ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LHand")
        {
            Destroy(transform.parent.gameObject);
            NoteMaking.L_perfect++;  
            L_p_state = "LPerfect";
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
