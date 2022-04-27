using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource kick;
    public AudioSource hc;
    public AudioSource ho;
    public AudioSource snare;
    public AudioSource tom;
    public AudioSource crash;

    public AudioSource music;
    public DetermineController determineController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        
    }

    public void PlayKick()
    {
        kick.Play();
    }
    public void PlaySnare()
    {
        snare.Play();
    }
    public void PlayHihatOpen()
    {
        ho.Play();
    }

    public void PlayHihatClose()
    {
        hc.Play();
    }

    public void PlayCrash()
    {
        crash.Play();
    }

    public void PlayRing()
    {
        
    }

    public void PlayHiTom()
    {
        
        
    }

    public void PlayFloorTom()
    {
        tom.Play();
    }
    
    public void PlayMusic()
    {
        music.Play();
    }
}
