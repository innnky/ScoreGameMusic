using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    // Crash
    // Ring
    // HiTom
    // FloorTom
    // Kick
    // Snare
    // HihatClose
    // HihatOpen
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI timeText;
    public int aa = 2;
    private AudioController audioController;
    private DetermineController determineController;
    private TimerScript _timerScript;
    // private TextMeshProUGUI timeText;
    // public 
    // Start is called before the first frame update
    void Start()
    { 
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        determineController = GameObject.Find("DetermineController").GetComponent<DetermineController>();
        _timerScript = GameObject.Find("TimeController").GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = "200";
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            KickTriggered();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SnareTriggered();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            HihatCloseTriggered();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            HihatOpenTriggered();
        }
        
    }

    private void FixedUpdate()
    {
        timeText.SetText(_timerScript.GetTime().ToString());
    }

    public void KickTriggered()
    {
        audioController.PlayKick();
        determineController.KeyTriggered(0);
    }
    
    public void SnareTriggered()
    {
        audioController.PlaySnare();
        determineController.KeyTriggered(1);
    }
    
    // hihatOpen
    public void HihatOpenTriggered()
    {
        audioController.PlayHihatOpen();
        determineController.KeyTriggered(3);
    }
    //hihatClose
    public void HihatCloseTriggered()
    {
        audioController.PlayHihatClose();
        determineController.KeyTriggered(2);
    }
    
    //crash
    public void CrashTriggered()
    {
        audioController.PlayCrash();
        determineController.KeyTriggered(4);
    }
    
    //ring
    public void RingTriggered()
    {
        audioController.PlayRing();
        determineController.KeyTriggered(5);
    }
    
    //hitom
    public void HiTomTriggered()
    {
        audioController.PlayHiTom();
        determineController.KeyTriggered(6);
    }
    
    //floortom
    public void FloorTomTriggered()
    {
        audioController.PlayFloorTom();
        determineController.KeyTriggered(7);
    }

    public void startTimer()
    {
        _timerScript.StartTiming();
    }
}
