using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UIElements;

public class SMPL_Move : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    
    bool running;
    public static string pose_info;

    private GameObject Hip; //선형이동, 몸의 중심
    private GameObject LeftHipJoint; //좌측 고관절LowManLeftUpLeg, xyz
    private GameObject RightHipJoint; //우측 고관절
    private GameObject LeftKnee; //LowManLeftUpLeg, x
    private GameObject RightKnee;
    private GameObject Waist; //허리돌리기x, z, LowManSpine1
    private GameObject UpperBody; //어께위치 바꾸기 x, z, LowManSpine2
    private GameObject LeftShoulder; //y방향 rotation만 하기(팔 들기)LowManLeftShoulder
    private GameObject LeftShoulder2; //어께LowManLeftArm, x와 z
    private GameObject LeftElbow; //팔꿈치, x방향, LowManLeftForeArm
    private GameObject RightShoulder;
    private GameObject RightShoulder2;
    private GameObject RightElbow;
    private GameObject Head;
    private GameObject LeftWrist;
    private GameObject RightWrist;
    private GameObject LeftAnkle;
    private GameObject RightAnkle;


    private float[] Angles = new float[20];
    public float[] LearningRates = new float[13];


    static public Vector3[] ImportedData = new Vector3[13];


    private Vector3[] Pos = new Vector3[13];

    public float SamplingDelta = 1f;
    
    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();
        client = listener.AcceptTcpClient();
        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }
    
    void SendAndReceiveData()
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string
        
        
        if (dataReceived != null)
        {
            string[] sArray = dataReceived.Split(',');
            for (int i = 0; i < ImportedData.Length; i++)
            {
                ImportedData[i] =  new Vector3(-float.Parse(sArray[i*3])+0.5f, -float.Parse(sArray[i*3 + 1])+3f, -float.Parse(sArray[i*3 + 2]));
            }
            pose_info = sArray[sArray.Length-1];
        }
    }
    
    private void Awake() 
    {
        int size = FindObjectsOfType<SMPL_Move>().Length;
        if (size != 1)
        {
            Destroy(gameObject); 
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
          
    }
    
    void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.IsBackground = true;
        mThread.Start();
        
        
        Hip = GameObject.Find("LowManHips"); //선형이동, 몸의 중심
        LeftHipJoint = GameObject.Find("LowManLeftUpLeg"); //좌측 고관절LowManLeftUpLeg, xyz
        RightHipJoint = GameObject.Find("LowManRightUpLeg"); //우측 고관절
        LeftKnee = GameObject.Find("LowManLeftLeg"); //LowManLeftLeg, x
        RightKnee = GameObject.Find("LowManRightLeg");
        Waist = GameObject.Find("LowManSpine1"); //허리돌리기x, z, LowManSpine1
        UpperBody = GameObject.Find("LowManSpine2"); //어께위치 바꾸기 x, z, LowManSpine2
        LeftShoulder = GameObject.Find("LowManLeftShoulder"); //y방향 rotation만 하기(팔 들기)LowManLeftShoulder
        LeftShoulder2 = GameObject.Find("LowManLeftArm"); //어께LowManLeftArm, x와 z
        LeftElbow = GameObject.Find("LowManLeftForeArm"); //팔꿈치, x방향, LowManLeftForeArm
        RightShoulder = GameObject.Find("LowManRightShoulder");
        RightShoulder2 = GameObject.Find("LowManRightArm");
        RightElbow = GameObject.Find("LowManRightForeArm");

        Head = GameObject.Find("LowManHead_end");
        LeftWrist = GameObject.Find("LowManLeftHand");
        RightWrist = GameObject.Find("LowManRightHand");
        LeftAnkle = GameObject.Find("LowManLeftFoot");
        RightAnkle = GameObject.Find("LowManRightFoot");
        
        SMPLPose();
        
        for (int i = 0; i < 13; i++)
        {
            ImportedData[i] = Pos[i];
        }
        
        for (var i = 0; i < Angles.Length; i++)
            Angles[i] = 0f;
    }

    public void Rotating()
    {
        LeftHipJoint.transform.Rotate(Angles[0], Angles[1], Angles[2]);
        RightHipJoint.transform.Rotate(Angles[3], Angles[4], Angles[5]);
        LeftKnee.transform.Rotate(Angles[6], 0f, 0f);
        RightKnee.transform.Rotate(Angles[7], 0f, 0f);
        Waist.transform.Rotate(Angles[8], 0f, Angles[9]);
        UpperBody.transform.Rotate(Angles[10], 0f, Angles[11]);
        LeftShoulder.transform.Rotate(0f, Angles[12], 0f);
        LeftShoulder2.transform.Rotate(-Angles[13], 0, -Angles[14]);
        LeftElbow.transform.Rotate(-Angles[15], 0f, 0f);
        RightShoulder.transform.Rotate(0f, Angles[16], 0f);
        RightShoulder2.transform.Rotate(Angles[17], 0, Angles[18]);
        RightElbow.transform.Rotate(Angles[19], 0f, 0f);

        return;
    }

    public void SMPLPose()
    {
        Pos[0] = Head.transform.position;
        Pos[1] = new Vector3(LeftShoulder.transform.position.x,LeftShoulder.transform.position.y,-LeftShoulder.transform.position.z);
        Pos[2] = new Vector3(RightShoulder.transform.position.x,RightShoulder.transform.position.y,-RightShoulder.transform.position.z);
        Pos[3] = LeftElbow.transform.position;
        Pos[4] = RightElbow.transform.position;
        Pos[5] = LeftWrist.transform.position; 
        Pos[6] = RightWrist.transform.position;
        Pos[7] = LeftHipJoint.transform.position;
        Pos[8] = RightHipJoint.transform.position;
        Pos[9] = LeftKnee.transform.position;
        Pos[10] = RightKnee.transform.position;
        Pos[11] = LeftAnkle.transform.position;
        Pos[12] = RightAnkle.transform.position;
    }
    
    public void ForwardKinematics()
    {
        Rotating();
        SMPLPose();
        return;
    }

    public float[] Delta(Vector3[] dest)//, float[] Angles)
    {
        ForwardKinematics();
        var Distances = new float[13];

        for (int i = 0; i < 13; i++)
            Distances[i] = Vector3.Distance(Pos[i], dest[i]);

        return Distances;
    }


    public float[] PartialGradient(Vector3[] dest, int i)//, float[] Angles, int i)
    {
        float[] Before = Delta(dest);
        Angles[i] = SamplingDelta;
        
        float[] After = Delta(dest);

        var gradient = new float[After.Length];

        for (var a = 0; a < gradient.Length; a++)
        {
                gradient[a] = (After[a] - Before[a]) / SamplingDelta;
        }
        
        Angles[i] = -SamplingDelta;
        Rotating();
        
        return gradient;
    }

    public void InverseKinematics()//, float[] Angles)
    {
        for (int i = 0; i<Angles.Length; i++)
        {
             if (i > 11)
             {
                float[] gradient = PartialGradient(ImportedData, i);
                Angles[i] = 0f;
                for (int a = 0; a<LearningRates.Length; a++)
                {
                    Angles[i] -= LearningRates[a] * gradient[a];
                }
                Rotating();
                Angles[i] = 0f;
             }
        }
        
        return;
    }

    // Update is called once per frame
    void Update()
    {
        Hip.transform.position = Vector3.Lerp(Hip.transform.position, (ImportedData[7] + ImportedData[8]) / 2, Time.deltaTime*10f);
        InverseKinematics();
    
    }
}
