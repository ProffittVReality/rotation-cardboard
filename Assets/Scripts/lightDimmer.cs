using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightDimmer: MonoBehaviour {

    public Light lt = null;
    public bool playClick;
    
    private float intensity;
    private float fadeSpeed;

    public float fadeTime;
    public float delay;

    public Button playButton;

    // Use this for initialization
    void Start () {
        lt = GetComponent<Light>();
        intensity = lt.intensity;
        delay = 2;
        fadeTime = 2;


        fadeTime = Mathf.Abs(fadeTime);

        if (fadeTime > 0.0)
        {
            fadeSpeed = intensity / fadeTime;
        }
        else
        {
            fadeSpeed = intensity;
        }
        //alpha = 1.0;
        playButton.onClick.AddListener(setPlayClick);
    }


    void setPlayClick()
    {
        playClick = true;
    }

// Update is called once per frame
    void Update () {
    
        if (playClick)
        { 
            if (intensity > 0.0)
            {
                intensity -= fadeSpeed * Time.deltaTime;
                lt.intensity = intensity;
            }
        }
            //lt.intensity = Mathf.Lerp(lowIntensity, highIntensity, fadeSpeed * Time.deltaTime);
    }


}
