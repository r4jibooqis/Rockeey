using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketColiision : MonoBehaviour
{  
    //Object
    AudioSource audioSource;

    //inspector variables 
    [SerializeField] float RespawnDelay = 1f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip Explosion;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    //variables
    bool stopSound = false;
    bool stopCollision = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update ()
    {
        DebugKeyBinding();
    }

    void DebugKeyBinding()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            Debug.Log("Load Next Level");
            LoadNextSLevel();
        }
        if(Input.GetKeyDown(KeyCode.C)){
            stopCollision = !stopCollision;
        }
    }

    //methods
    private void OnCollisionEnter(Collision collision)
    {
        if(stopSound || stopCollision){
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Fuel":
                break;
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Finish":
                FinishLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence ()
    { 
        stopSound = true;
        //disable movement script
        gameObject.GetComponent<Movement>().enabled = false;
        //play explosiont sound
        audioSource.Stop();
        audioSource.PlayOneShot(Explosion,1f);
        //play Explosion effect
        crashParticle.Play();
        //Delay in Respawn
        Invoke("reloadLevel",RespawnDelay);
    }

    void FinishLevel()
    {   
        stopSound = true;
        //disable movement script
        gameObject.GetComponent<Movement>().enabled = false;
        //play successSound sound
        audioSource.Stop();
        audioSource.PlayOneShot(successSound,1f);
        //play success effect
        successParticle.Play();
        //Delay in Respawn
        Invoke("LoadNextSLevel",RespawnDelay);
    }

    void LoadNextSLevel()
    {
        int currenIndexScene = SceneManager.GetActiveScene().buildIndex;
        int NextIndexScene = currenIndexScene + 1;

        if (NextIndexScene == SceneManager.sceneCountInBuildSettings)
            NextIndexScene = 0;

        SceneManager.LoadScene(NextIndexScene);
    }

    void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter (Collider other){
        if(other.gameObject.CompareTag("Fuel")){
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }


}
