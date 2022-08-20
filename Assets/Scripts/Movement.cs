using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Object
    Rigidbody rigidRocket;
    AudioSource lunchAudio;


    //Variables
    [SerializeField]float MainThrust = 1;
    [SerializeField]float RotationThrust = 1;
    [SerializeField]AudioClip mainEngine;
    [SerializeField]ParticleSystem JetThrustParticle;
    [SerializeField]ParticleSystem RSideThrustParticle;
    [SerializeField]ParticleSystem LSideThrustParticle;

    // Start is called before the first frame update
    void Start()
    {
        rigidRocket = GetComponent<Rigidbody>();
        lunchAudio = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // method for thrust the rocket
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }

    }

    // method to rotate the rocket
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    //////////////////////////////////////////////////////////////////////////
    
    private void StartThrust()
    {
        //make rokcet fly
        rigidRocket.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
        //Main Thrust Particle Affect
        if (!JetThrustParticle.isPlaying)
        {
            JetThrustParticle.Play();
        }
        //Thrusting sound
        if (!lunchAudio.isPlaying)
        {
            lunchAudio.PlayOneShot(mainEngine, 1f);
        }
    }

    private void StopThrust()
    {
        lunchAudio.Stop();  //stop thrust sound
        JetThrustParticle.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(RotationThrust);
        if (!RSideThrustParticle.isPlaying)
        {
            RSideThrustParticle.Play(); //play right thrust effect
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-RotationThrust);
        if (!LSideThrustParticle.isPlaying)
        {
            LSideThrustParticle.Play(); //play Left thrust effect
        }
    }

    private void StopRotate()
    {
        RSideThrustParticle.Stop();
        LSideThrustParticle.Stop();
    }

    //method to apply Rotation
    private void ApplyRotation(float RotationSpeed)
    {
        rigidRocket.freezeRotation = true;  //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
        rigidRocket.freezeRotation = false;
    }


}
