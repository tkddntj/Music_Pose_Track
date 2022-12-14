using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    float time = 0f;
    // Start is called before the first frame update
    private void Update() {
        if(SMPL_Move.pose_info == "START")
        {
            time += Time.deltaTime;
            if(time > 5 && SMPL_Move.pose_info == "START")
            {
                time = 0f;
                SceneManager.LoadScene("Game");
            }
        }
    }
}
