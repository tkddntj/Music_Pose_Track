using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Scene관리를 위한 것

public class Game_End : MonoBehaviour
{
    int count = 0;
    void Update() 
    {
        int Sum = NoteMaking.miss+NoteMaking.R_good+NoteMaking.R_perfect+
        NoteMaking.L_good+NoteMaking.L_perfect;
        if( Sum== 49)
        {
            if(SMPL_Move.pose_info == "LEFT")
            {
                count = 1;
            }
            if(SMPL_Move.pose_info == "RIGHT" && count == 1)
            {
                SceneManager.LoadScene("End"); 
                count=0;
            }

        }
    }

}