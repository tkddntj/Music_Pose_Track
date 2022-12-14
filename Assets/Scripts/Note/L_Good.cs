using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Good : MonoBehaviour
{
    public static int L_Go_Count = 0;
    public static string L_Go_state ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LHand")
        {
            Destroy(transform.parent.gameObject);
            NoteMaking.L_good++;
            L_Go_Count++;
            L_Go_state = "LGood";
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
