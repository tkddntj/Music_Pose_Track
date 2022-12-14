using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteMaking : MonoBehaviour
{
    public GameObject RNote;
    public GameObject LNote;
    private StreamReader R_data;
    private StreamReader L_data;

    
    public static int R_perfect = 0;
    public static int R_good = 0;
    public static int L_perfect = 0;
    public static int L_good = 0;
    public static int miss = 0;
    public static int score = 0;
    
    public static float speed = 5; //1초에 이만큼 이동
    public static float missPos = -speed * 0.5f; //perfect 150ms, great 150ms, good 200ms


    private float start_time;
    private float prog_time;
    
    public float appear_pos = 5; //z = -100에서 노트 나타남
    private float time_offset; // appear_pos / speed
    
    private float[] tempdata_R = new float[3];
    private float[] tempdata_L = new float[3];

    private void loadData_R() //한줄 읽어 tempdata에 저장
    {
        if (R_data.EndOfStream == true){
            for (var i = 0; i < 3; i++)
            {
                tempdata_R[i] = 0;
            }
            return;
        }
        var line = R_data.ReadLine();
        var csv_tempdata_R = line.Split(',');
        for (var i = 0; i < 3; i++)
        {
            tempdata_R[i] = float.Parse(csv_tempdata_R[i]);
        }
    }
    
    private void loadData_L() //한줄 읽어 tempdata에 저장
    {
        if (L_data.EndOfStream == true){
            for (var i = 0; i < 3; i++)
            {
                tempdata_L[i] = 0;
            }
            return;
        }
        var line = L_data.ReadLine();
        var csv_tempdata_L = line.Split(',');
        for (var i = 0; i < 3; i++)
        {
            tempdata_L[i] = float.Parse(csv_tempdata_L[i]);
        }
    }

    private void NoteSet(float zpos, bool isRight)
    {
        if (isRight)
        {
            Instantiate(RNote, new Vector3(tempdata_R[0]*2, tempdata_R[1]+1.9f, zpos), Quaternion.identity, transform);
            loadData_R();
        }
        else
        {
            Instantiate(LNote, new Vector3(tempdata_L[0]*2, tempdata_L[1]+1.9f, zpos), Quaternion.identity, transform);
            loadData_L();
        }
    }

    private void Left_Double_Check()
    {
        int Check_Left_Perfect = L_Good.L_Go_Count + L_Perfect.L_p_Count;

        if(Check_Left_Perfect ==2)
        {
            L_good--;
            L_Good.L_Go_Count = 0;
            L_Perfect.L_p_Count = 0;
            L_Good.L_Go_state = " ";
            L_Perfect.L_p_state = " ";
        }    
        else
        {
            L_Good.L_Go_Count = 0;
            L_Perfect.L_p_Count = 0;
            L_Good.L_Go_state = " ";
            L_Perfect.L_p_state = " ";
        }
    }

    private void Right_Double_Check()
    {
        int Check_Right_Perfect1 = R_Good.R_Go_Count + R_Perfect.R_p_Count;

        if(Check_Right_Perfect1 ==2)
        {
            R_good--;
            R_Good.R_Go_Count = 0;
            R_Perfect.R_p_Count = 0;
            R_Good.R_Go_state = " ";
            R_Perfect.R_p_state = " ";
        }    
        else
        {
            R_Good.R_Go_Count = 0;
            R_Perfect.R_p_Count = 0;
            R_Good.R_Go_state = " ";
            R_Perfect.R_p_state = " ";
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        R_data = new StreamReader(Application.dataPath + "/" + "place_R.csv");
        L_data = new StreamReader(Application.dataPath + "/" + "place_L.csv");
        start_time = Time.time;
        time_offset = -(appear_pos / speed);
        
        loadData_R();
        loadData_L();
    }

    // Update is called once per frame
    void Update()
    {
        prog_time = Time.time - start_time;
        
        if (tempdata_R[2] != 0 & tempdata_R[2] < prog_time + time_offset) //appear_pos 안에 노트가 나타나야 할 시간이라면
            NoteSet((tempdata_R[2] - time_offset) * speed, true);
        if (tempdata_L[2] != 0 & tempdata_L[2] < prog_time + time_offset)
            NoteSet((tempdata_L[2] - time_offset) * speed, false);

        Left_Double_Check();
        Right_Double_Check();

        score = R_perfect * 100 + R_good * 30 + L_perfect * 100 + L_good * 30;
    }
}
