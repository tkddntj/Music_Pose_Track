using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public static int MissCount=0;
    public static string miss_state;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * -NoteMaking.speed));
        if (transform.localPosition.z < NoteMaking.missPos)
        {
            Destroy(gameObject);
            NoteMaking.miss++;
            miss_state = "Miss";
        }
    }
}
