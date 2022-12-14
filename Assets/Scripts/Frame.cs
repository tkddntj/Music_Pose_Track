using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    [Range(1,144)]  // 게임 프레임의 최소 최대치 범위를 지정한다. 
    public int rate = 60;   //프레임을 60으로 초기설정하고, public이라 임의의 값으로 수정가능하다. 
    void Start()
    {
        Application.targetFrameRate = rate; // 설정된 값으로 Application의 프레임 값이 설정된다. 
    }
}
