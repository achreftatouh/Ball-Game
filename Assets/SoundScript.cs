using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundScript : MonoBehaviour
{
    static SoundScript instance = null;
    //Reference to Audio Source component
    // Music volume variable that will be modified
    // by dragging slider knob
    private float musicVolume = 1f;
    private AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {

        // Assign Audio Source component to control it
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // Setting volume option of Audio Source to be equal to musicVolume
        audioSrc.volume = musicVolume;
    }

    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
 

   
}
