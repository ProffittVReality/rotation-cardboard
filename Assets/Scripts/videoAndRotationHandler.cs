using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using System.IO;


public class videoAndRotationHandler : MonoBehaviour
{

    public RawImage image;
    public RawImage questionnaire;
    public Canvas questionScreen;
    public Image fadePanel;

    public VideoClip videoToPlay1;
    public VideoClip videoToPlay2;
    public VideoClip videoToPlay5;
    public VideoClip videoToPlay10;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;

    public double speed1 = 0.1;
    public double speed2 = 0.2;
    public double speed3 = 0.4;
    public double speed4 = 1;



    public bool startRotate = true;
    public bool turnLeft;
    //public float rotateSpeed;
    private int listSize = 3;
    public Text videoSet;
    public Text speedLength;
    public float rotationOffset = 0;
    public string movieLength ="";
    public string rotationSpeed="";

    public string countFileName = "";
    public string dataFileName= "";
    public Button exportCount;
    public Button playButton;
    string count;

    List<Condition> conditionList = new List<Condition>();
    List<Condition> conditionListA = new List<Condition>();
    List<Condition> conditionListB = new List<Condition>();
    List<Condition> conditionListC = new List<Condition>();
    List<Condition> conditionListD = new List<Condition>();
    Condition myConditions;

    /*public GameObject leftController;
    public GameObject rightController;

    GameObject laserScript;


    public GameObject rightLaser;
    public GameObject leftLaser;
 	*/
    public GameObject cameraEye;


    public float previousDistance;
    public float currentDistance;
    public float deltaDistance;
    public float maxDistance;
    public float endDistance;
    public float temp;

    public int countRotations;




    private void Start()
    {
        turnLeft = true;
        GameObject guiObject = GameObject.FindWithTag("GUI");
        GUI_Handler guiHandler = guiObject.GetComponent<GUI_Handler>();
        dataFileName = guiHandler.FileName;

        previousDistance = 0;

        if (dataFileName.Equals(""))
        {
            dataFileName = "default";
        }
        speedLength.text = " empty ";

        startRotate = false;
        Application.runInBackground = true;
        transform.eulerAngles = new Vector3(0, 0, 0);
        image.enabled = false;
        questionScreen.enabled = false;
        fadePanel.enabled = false;
        setConditions();

        if (countFileName.Equals(""))
        {
            countFileName = "countFile.txt";
        }
        readData();
        exportCount.onClick.AddListener(writeData);
        playButton.onClick.AddListener(nextVideo);


    }

    void Update()
    {
        if (previousDistance == 0)
        {
            previousDistance = cameraEye.transform.localEulerAngles.y;
        }
        if (startRotate && this.gameObject.CompareTag("MainCamera") && !turnLeft)
        {
            transform.Rotate(Vector2.up, Time.deltaTime * (float)myConditions.speed);          
            currentDistance = cameraEye.transform.localEulerAngles.y;
            if ((currentDistance - previousDistance) > 200)
            {
                currentDistance -= 360;
            }
            if (maxDistance < currentDistance) //checks if the current rotation is more than a previous
            {
                //maxDistance = temp;
                deltaDistance += currentDistance - maxDistance;
                maxDistance = currentDistance; //updates if this is true
               
            } if (maxDistance > currentDistance && maxDistance - currentDistance > 350 && deltaDistance > 300)
            {
                deltaDistance += (currentDistance - maxDistance) + 360;
                maxDistance = currentDistance;
            }
        }
        if (startRotate && this.gameObject.CompareTag("MainCamera") && turnLeft)
        {
            transform.Rotate(Vector2.down, Time.deltaTime * (float)myConditions.speed);
            currentDistance = cameraEye.transform.localEulerAngles.y;
            currentDistance -= 360;
            if ((currentDistance - previousDistance) < 200 )
            {
                currentDistance -= 360;
            }
            if (maxDistance < currentDistance) //checks if the current rotation is more than a previous
            {
                maxDistance = currentDistance; //updates if this is true
            }

        }
        previousDistance = currentDistance;
    }


        //called after participant clicks done in their questionnaire
        //disables the question screen, enables the image to play the video, sets a new length and speed
        //starts rotating and plays video
        //switches the direction of rotation
        public void nextVideo()
    {
        //previousDistance = cameraEye.transform.rotation.eulerAngles.y;
        /*leftLaser.SetActive(false);
        rightLaser.SetActive(false);
        leftController.SetActive(false);
        rightController.SetActive(false);
        */
        questionScreen.enabled = false;
        image.enabled = true;
        setLengthAndSpeed();
        startRotate = true;
        if (turnLeft)
        {
            turnLeft = false;
        } else
        {
            turnLeft = true;
        }
        deltaDistance = 0;
        StartCoroutine(playVideo());
    }


    //reads from a file to decide which set of videos and rotation speeds to choose from, switches to next letter 
    //each time export is clicked A->B->C->D->A...
    string readData()
    {
        //string count;
        string path = @"Assets\CountFolder\" + countFileName + ".txt";
        //initialize file with A as the chosen list
        if (!File.Exists(path))
        {
            count = "A";
            File.WriteAllText(path, count);
        } 
        count = File.ReadAllText(path);
      
        if (count.Equals("A")) {
            chooseList("A");
        }
        else if (count.Equals("B")) {
            chooseList("B");
        }
        else if (count.Equals("C")) {
            chooseList("C");
        }
        else if (count.Equals("D")) {
            chooseList("D");
        }
        return count;

    }

    //when export is clicked this is called, reads the countFile and switches the letter
    void writeData()
    {
        string letter = " ";
        if (readData().Equals("A")) {
            letter = "B";
        } else if (readData().Equals("B")) {
            letter = "C";
        }
        else if (readData().Equals("C")) {
            letter = "D";
        }
        else if (readData().Equals("D")) {
            letter = "A";
        }

        string path = @"Assets\CountFolder\" + countFileName + ".txt";
        string dataPath = @"Assets\Data\" + dataFileName + ".txt";
        if (!File.Exists(path))
        {
            return;
        }
        File.WriteAllText(path, letter);
        File.AppendAllText(dataPath, "\t" + videoSet.text );

    }

    public class Condition
    {
        public double speed;
        public int movLength;
        public Condition(double speed, int movLength)
        {
            this.speed = speed;
            this.movLength = movLength;
        }
        
    }

    //creates the list of conditions (A,B,C, or D) 
    void setConditions()
    {
        //make list A of conditions 
        Condition oneA = new Condition(speed1, 1);
        Condition twoA = new Condition(speed2, 2);
        Condition threeA = new Condition(speed3, 5);
        Condition fourA = new Condition(speed4, 10);
        //List<Condition> conditionList = new List<Condition>();
        conditionListA.Add(oneA);
        conditionListA.Add(twoA);
        conditionListA.Add(threeA);
        conditionListA.Add(fourA);
        
        //make list B of conditions
        Condition oneB = new Condition(speed2, 1);
        Condition twoB = new Condition(speed3, 2);
        Condition threeB = new Condition(speed4, 5);
        Condition fourB = new Condition(speed1, 10);
        conditionListB.Add(oneB);
        conditionListB.Add(twoB);
        conditionListB.Add(threeB);
        conditionListB.Add(fourB);

        //make list of C conditions
        Condition oneC = new Condition(speed3, 1);
        Condition twoC = new Condition(speed4, 2);
        Condition threeC = new Condition(speed1, 5);
        Condition fourC = new Condition(speed2, 10);
        conditionListC.Add(oneC);
        conditionListC.Add(twoC);
        conditionListC.Add(threeC);
        conditionListC.Add(fourC);

        //make list of D condition
        Condition oneD = new Condition(speed4, 1);
        Condition twoD = new Condition(speed1, 2);
        Condition threeD = new Condition(speed2, 5);
        Condition fourD = new Condition(speed3, 10);
        conditionListD.Add(oneD);
        conditionListD.Add(twoD);
        conditionListD.Add(threeD);
        conditionListD.Add(fourD);


    }


    //chooses which letter list will be used for the trial (A,B,C, or D)
    void chooseList(string letter)
    {
        //System.Random rand = new System.Random();
        //int chooseSet = rand.Next(1, 5);
        switch (letter)
        {
            case "A":
                conditionList = conditionListA;
                videoSet.text = "Condition Set A";
                break;
            case "B":
                conditionList = conditionListB;
                videoSet.text = "Condition Set B";
                break;
            case "C":
                conditionList = conditionListC;
                videoSet.text = "Condition Set C";
                break;
            case "D":
                conditionList = conditionListD;
                videoSet.text = "Condition Set D";
                break;
        }
    }

    //chooses the length and speed for the individual video of 4 videos for each trial, returns a Condition
    Condition chooseLengthAndSpeed()
    {
        System.Random myRand = new System.Random();
        int chooseCondition = myRand.Next(0, listSize);
        Condition temp = conditionList[chooseCondition];
        conditionList.Remove(temp);
        movieLength = temp.movLength.ToString();
        rotationSpeed = temp.speed.ToString();
        //rotationAndLength.text = "(Rotation Speed, Video Length): (" + rotationSpeed + ", " + movieLength + ")";
        speedLength.text = "(" + rotationSpeed + ", " + movieLength + ")";
        listSize--;
        return temp;

    }
    
    // sets myConditions equal to the condition returned from chooseLengthAndSpeed()
    void setLengthAndSpeed()
    {
        myConditions = chooseLengthAndSpeed();
    }


    //sets the videoPlayer, audioSource, according to the condition
    //when movie is done the fadePanel is turned on and faded to black
    //should be rotated to look at screen (maybe) and the questions should be in front of them
    IEnumerator playVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.isLooping = false;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        
        //Condition myConditions = chooseLengthAndSpeed();

        //Set video To Play then prepare Audio to prevent Buffering
        //set videoPlayer.clip to correct video
        if (myConditions.movLength == 1)
        {
            videoPlayer.clip = videoToPlay1;
        }
        else if (myConditions.movLength == 2)
        {
            videoPlayer.clip = videoToPlay2;
        }
        else if (myConditions.movLength == 5)
        {
            videoPlayer.clip = videoToPlay5;
        }
        else if (myConditions.movLength == 10)
        {
            videoPlayer.clip = videoToPlay10;
        }

        videoPlayer.Prepare();

        WaitForSeconds waitTime = new WaitForSeconds(2); //wait until vid is prepared
        while (!videoPlayer.isPrepared)
        {
            //Prepare/Wait for 2 sceonds only
            yield return waitTime; //Break out of loop after 2 seconds
            break;
        }

        image.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
        
        yield return StartCoroutine(Wait((float)videoPlayer.clip.length)); // wait till clip is done playing


        endDistance = cameraEye.transform.localEulerAngles.y;
        startRotate = false; //stop rotation
        Debug.Log("stop rotation");
        float pastOffset = rotationOffset;
        float currOffset = transform.eulerAngles.y;
        //rotationOffset = currOffset-pastOffset;
        //rotationOffset = Math.Abs(rotationOffset);
        //deltaDistance = currentDistance - previousDistance;

        /*rightLaser.SetActive(true);
        leftLaser.SetActive(true);

        leftController.SetActive(true);
        rightController.SetActive(true);
		*/

        image.enabled = false;
        fadePanel.enabled = true;
        fadePanel.CrossFadeAlpha(255, 5f, false); //darken screen
        yield return StartCoroutine(Wait(2f));

        //transform.eulerAngles = new Vector3(0, 0, 0); //rotate back to origin
        yield return StartCoroutine(Wait(5f));
        fadePanel.CrossFadeAlpha(1, 7f, false); //lighten theater
        yield return StartCoroutine(Wait(5f));
        questionScreen.enabled = true;

    }

    private IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }




    public string getVideoText()
    {
        return this.videoSet.text;
    }

    public string getConditionsText()
    {
        return this.speedLength.text;
    }



}
