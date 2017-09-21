using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationDrift : MonoBehaviour {

    public float tiltAngle;
    public float smooth;
    public bool startRotate;
    public float rotateSpeed;
    private void Start()
    {
        startRotate = false;
    }

    public class Condition
    {
        public double speed;
        public int movLength;
        public Condition (double speed, int movLength)
        {
            this.speed = speed;
            this.movLength = movLength;
        }

    }


    void setConditions()
    {
        Condition one = new Condition(0.1, 1);
        Condition two = new Condition(0.2, 2);
        Condition three = new Condition(0.4, 5);
        Condition four = new Condition(1, 10);
        List <Condition> conditionList = new List<Condition>();
        conditionList.Add(one);
        conditionList.Add(two);
        conditionList.Add(three);
        conditionList.Add(four);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
  
            startRotate = true;
            Debug.Log("Key was pressed");
        }
        if (startRotate && this.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("starting rotation");
            transform.Rotate(Vector2.up, Time.deltaTime * rotateSpeed);
        }
    }
}

