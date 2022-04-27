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
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI comboIcon;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public int aa = 2;
    private AudioController audioController;
    private DetermineController determineController;
    private TimerScript _timerScript;
    
    public bool isCombo = false;
    public int combo = 0;
    public int score = 0;
    
    // private TextMeshProUGUI timeText;
    // public 
    // Start is called before the first frame update
    void Start()
    { 
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        determineController = GameObject.Find("DetermineController").GetComponent<DetermineController>();
        _timerScript = GameObject.Find("TimeController").GetComponent<TimerScript>();
        timeText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
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
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            CrashTriggered();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            FloorTomTriggered();
        }

    }

    private void FixedUpdate()
    {
        if (isCombo)
        {
            comboIcon.enabled = true;
            comboText.enabled = true;
            comboText.text = combo.ToString();
        }
        else
        {
            comboIcon.enabled = false;
            comboText.enabled = false;
        }    
        scoreText.text = score.ToString();
        
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
        StartCoroutine(delay3s());
    }

    private IEnumerator delay3s()
    {
        yield return new WaitForSeconds(0);
        _timerScript.StartTiming();
        audioController.PlayMusic();
    }
    
    // public void SetCombo(int combo)
    // public void SetCombo(int combo)
    // {
    //     
    //     textMeshProUGUI.text = combo.ToString();
    // }
    public void addCombo()
    {
        combo++;
        if (combo>1)
        {
            isCombo = true;      
        }
        
    }
    public void clearCombo()
    {
        combo = 0;
        isCombo = false;
    }
    public void addScore(int score)
    {
        this.score += score;
    }
}
