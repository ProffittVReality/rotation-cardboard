using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.EventSystems;

public class questionnaireHandler : MonoBehaviour
{

    public GameObject cameraObject;
    videoAndRotationHandler rotationHandler;

    public GameObject questionnaire;
    public string questionFileName;
    public string dataFileName = "";
    private string rotAndLength = "empty";
    private string rotationSpeed = "null";
    private string movieLength = "null";

    private float rotOffset = 0;


    public Text val1;
    public Text val2;
    public Text val3;
    public Text val4;
    

    float num1;
    float num2;
    float num3;
    float num4;

    public Button done;
    //first set of buttons
    public Button b11;
    public Button b12;
    public Button b13;
    public Button b14;
    public Button b15;
    public Button b16;
    public Button b17;
    
    //second set of buttons
    public Button b21;
    public Button b22;
    public Button b23;
    public Button b24;
    public Button b25;
    public Button b26;
    public Button b27;

    //third set of buttons
    public Button b31;
    public Button b32;
    public Button b33;
    public Button b34;
    public Button b35;
    public Button b36;
    public Button b37;

    //fourth set of buttons
    public Button b41;
    public Button b42;
    public Button b43;
    public Button b44;
    public Button b45;
    public Button b46;
    public Button b47;




    // Use this for initialization
    void Start()
    {
        num1  =00;
        num2 = 00;
        num3 = 00;
        num4 = 00;

        //GameObject guiObject = GameObject.FindWithTag("GUI");
        //UI_Handler guiHandler = guiObject.GetComponent<GUI_Handler>();
        //dataFileName = guiHandler.FileName;

        if (dataFileName.Equals(""))
        {
            dataFileName = "default";
        }
        cameraObject = GameObject.FindWithTag("MainCamera");
        rotationHandler = cameraObject.GetComponent<videoAndRotationHandler>();
        //rotAndLength = rotationHandler.speedLength.text;

        done.onClick.AddListener(doneClicked);
        if (questionFileName.Equals(""))
        {
            questionFileName = "questionnaire";
        }

        b11.onClick.AddListener(numAnswer11);
        b12.onClick.AddListener(numAnswer12);
        b13.onClick.AddListener(numAnswer13);
        b14.onClick.AddListener(numAnswer14);
        b15.onClick.AddListener(numAnswer15);
        b16.onClick.AddListener(numAnswer16);
        b17.onClick.AddListener(numAnswer17);

        b21.onClick.AddListener(numAnswer21);
        b22.onClick.AddListener(numAnswer22);
        b23.onClick.AddListener(numAnswer23);
        b24.onClick.AddListener(numAnswer24);
        b25.onClick.AddListener(numAnswer25);
        b26.onClick.AddListener(numAnswer26);
        b27.onClick.AddListener(numAnswer27);

        b31.onClick.AddListener(numAnswer31);
        b32.onClick.AddListener(numAnswer32);
        b33.onClick.AddListener(numAnswer33);
        b34.onClick.AddListener(numAnswer34);
        b35.onClick.AddListener(numAnswer35);
        b36.onClick.AddListener(numAnswer36);
        b37.onClick.AddListener(numAnswer37);

        b41.onClick.AddListener(numAnswer41);
        b42.onClick.AddListener(numAnswer42);
        b43.onClick.AddListener(numAnswer43);
        b44.onClick.AddListener(numAnswer44);
        b45.onClick.AddListener(numAnswer45);
        b46.onClick.AddListener(numAnswer46);
        b47.onClick.AddListener(numAnswer47);
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("changing to the correct value for rotation and length");
        rotationSpeed = rotationHandler.rotationSpeed;
        movieLength = rotationHandler.movieLength;
        //rotAndLength = rotationHandler.speedLength.text;
        rotOffset = rotationHandler.deltaDistance;


        val1.text = num1.ToString();
        val2.text = num2.ToString();
        val3.text = num3.ToString();
        val4.text = num4.ToString();

        
    }


    void numAnswer11()
    { 
            num1 = 1;
    }
    void numAnswer12()
    {
            num1 = 2;
    }
    void numAnswer13()
    {
        num1 = 3;
    }
    void numAnswer14()
    {
        num1 = 4;
    }
    void numAnswer15()
    {
        num1 = 5;
    }
    void numAnswer16()
    {
        num1 = 6;
    }
    void numAnswer17()
    {
        num1 = 7;
    }

    void numAnswer21()
    {
        num2 = 1;
    }
    void numAnswer22()
    {
        num2 = 2;
    }
    void numAnswer23()
    {
        num2 = 3;
    }
    void numAnswer24()
    {
        num2 = 4;
    }
    void numAnswer25()
    {
        num2 = 5;
    }
    void numAnswer26()
    {
        num2 = 6;
    }
    void numAnswer27()
    {
        num2 = 7;
    }

    void numAnswer31()
    {
        num3 = 1;
    }
    void numAnswer32()
    {
        num3 = 2;
    }
    void numAnswer33()
    {
        num3 = 3;
    }
    void numAnswer34()
    {
        num3 = 4;
    }
    void numAnswer35()
    {
        num3 = 5;
    }
    void numAnswer36()
    {
        num3 = 6;
    }
    void numAnswer37()
    {
        num3 = 7;
    }

    void numAnswer41()
    {
        num4 = 1;
    }
    void numAnswer42()
    {
        num4 = 2;
    }
    void numAnswer43()
    {
        num4 = 3;
    }
    void numAnswer44()
    {
        num4 = 4;
    }
    void numAnswer45()
    {
        num4 = 5;
    }
    void numAnswer46()
    {
        num4 = 6;
    }
    void numAnswer47()
    {
        num4 = 7;
    }



    void doneClicked()
    {
        string path = @"Assets\Data\" + dataFileName + ".txt";
        if (!File.Exists(path))
        {
            string header = "Header for the answer to questions";
            File.WriteAllText(path, header + "\r\n");
        }


        string answer1 = num1.ToString();
        string answer2 = num2.ToString();
        string answer3 = num3.ToString();
        string answer4 = num4.ToString();
        
        File.AppendAllText(path, "\t" + rotationSpeed + "\t" + movieLength + "\t" + rotOffset.ToString() + "\t" + answer1 + "\t" + answer2 + "\t" + answer3 + "\t" + answer4 + "\t");
        num1 = 0;
        num2 = 0;
        num3 = 0;
        num4 = 0;

        rotationHandler.nextVideo();
    }

   
}
