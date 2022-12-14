using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class NoteAnim : MonoBehaviour
{
    public GameObject L_pe,L_go,R_pe,R_go,mi;
    Animator anim;  // 애니메이션을 위해 animator의 anim 설정한다.
    // Start is called before the first frame update
    void Awake() 
    {
        anim = GetComponent<Animator>();    // anim 값을 초기화한다.    
    }
    private void HideEffect1()
    {
        L_pe.gameObject.SetActive(false);
    }
    private void HideEffect2()
    {
        L_go.gameObject.SetActive(false);
    }
    private void HideEffect3()
    {
        R_go.gameObject.SetActive(false);
    }
    private void HideEffect4()
    {
        R_pe.gameObject.SetActive(false);
    }
    private void HideEffect5()
    {
        mi.gameObject.SetActive(false);
    }

    void Left_Animation()
    {
        if(L_Perfect.L_p_state == "LPerfect")
        {
            L_pe.gameObject.SetActive(true);
            Invoke("HideEffect1",0.35f);
            L_Perfect.L_p_state =" ";

                
        }
        if(L_Good.L_Go_state == "LGood")
        {
            L_go.gameObject.SetActive(true);
            Invoke("HideEffect2",0.35f);
            L_Good.L_Go_state =" ";
            
        }
        if(NoteMovement.miss_state == "Miss")
        {
            mi.gameObject.SetActive(true);
            Invoke("HideEffect5",0.35f);
            NoteMovement.miss_state =" ";
        }
    }
    void Right_Animation()
    {
        if(R_Perfect.R_p_state == "RPerfect")
        {
            R_pe.gameObject.SetActive(true);
            Invoke("HideEffect4",0.35f);
            R_Perfect.R_p_state = " ";
        }
        if(R_Good.R_Go_state == "RGood")
        {
            R_go.gameObject.SetActive(true);
            Invoke("HideEffect3",0.2f);
            R_Good.R_Go_state =" ";   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Right_Animation();
        Left_Animation();
    }
}
