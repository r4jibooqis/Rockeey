using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    //variables
    Vector3 startingPosition;
    [Range(0,1)] float movementFactor;
    [SerializeField] Vector3 movementvector;
    [SerializeField] float period = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovingObject();
    }

    private void MovingObject()
    {
        if(period <= Mathf.Epsilon){return;}
        //creating cycle
        float cycles = Time.time / period;  //continually growing over time

        const float tau = Mathf.PI * 2; //constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSinWave + 1) / 2;  // recalculated to go from 0 to 1 so its cleaner

        //moving object
        Vector3 offset = movementvector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
