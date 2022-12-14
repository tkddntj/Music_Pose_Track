using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Scene관리를 위한 것

public class Restart : MonoBehaviour
{
    float time = 0f;
    // Start is called before the first frame update
    private void Update() {
        if(SMPL_Move.pose_info == "START")
        {
            time += Time.deltaTime;
        }
        if(time > 5 && SMPL_Move.pose_info == "IDLE")
        {
            time = 0f;
            NoteMaking.score = 0;
            NoteMaking.R_perfect = 0;
            NoteMaking.R_good= 0;
            NoteMaking.L_perfect = 0;
            NoteMaking.L_good= 0;
            NoteMaking.miss = 0;
            SceneManager.LoadScene("Start");
        }
    }
}
