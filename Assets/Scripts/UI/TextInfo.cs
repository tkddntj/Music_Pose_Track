using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI 사용하기 위한 것

public class TextInfo : MonoBehaviour
{
    public enum Info {Volumes, score, How, finalscore, State} //  UI Text에 필요한 것들을 설정해준다. 
    public Info UiType; // 상황에 맞게 끔 UI 형태를 할당해준다. 
    Text myText;
    // Start is called before the first frame update
    void Awake()
    {
        myText = GetComponent<Text>();
        
    }
    // Update is called once per frame
    void LateUpdate()
    {

        switch(UiType)  // Switch를 이용해 상황에 맞게 관리해준다.
        {
            case Info.Volumes:
                myText.text = string.Format("{0:N0}",SoundManager.Volumes*100);    
                break;
            case Info.score:
                myText.text = "Score " + string.Format("{0:D3}",NoteMaking.score);    
                break;                                                                    
            case Info.How:
                myText.text = "perfect " + string.Format("{0}",NoteMaking.R_perfect+NoteMaking.L_perfect) +"\n"+"good " + 
                string.Format("{0}",NoteMaking.R_good+NoteMaking.L_good)+"\n"+"miss " + string.Format("{0}",NoteMaking.miss) ;    
                break;
            case Info.finalscore:
                myText.text = "High Score " + string.Format("{0:D3}",NoteMaking.score);      
                break;   
            case Info.State:
                myText.text = "상태 확인용 : " + SMPL_Move.pose_info;
                break;                                              
        }
    }
    
}
