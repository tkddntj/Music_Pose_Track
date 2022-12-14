using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Good : MonoBehaviour
{
    public static int R_Go_Count = 0;
    public static string R_Go_state ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RHand")
        {
            Destroy(transform.parent.gameObject);
            NoteMaking.R_good++;
            R_Go_Count++;
            R_Go_state = "RGood";
            other.SendMessage("PlaySound", NoteSound.Sfx.Hit);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, NoteMaking.speed * 0.4f);
        GetComponent<CapsuleCollider>().height = NoteMaking.speed * 0.2f;//0.2가 200ms
    }

}
