using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    private int startTime = 0;
    private bool isRunning = false;
    private bool isPaused = false;
    public TextMeshProUGUI textMeshProUGUI;

    private int pausedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTime()
    {
        if (isRunning)
        {
            if (isPaused)
            {
                return pausedTime-startTime;
            }
            var time = (int)(Time.time * 1000) - startTime;
            return time;
            
        }

        return 0;
    }

    public void StartTiming()
    {
        if (!isRunning)
        {
            startTime = (int)(Time.time * 1000);
            isRunning = true;
        }
        else
        {
            pause();
        }
    }
    public void StopTiming()
    {
        isRunning = false;
    }

    public void pause()
    {
        if (isPaused)
        {
            isPaused = false;
            startTime += (int)(Time.time * 1000) - pausedTime;
        }
        else
        {
            isPaused = true;
            pausedTime = (int)(Time.time * 1000);
        }
    }
    
    //isPaused
    public bool getPaused()
    {
        return isPaused;
    }
    
}
