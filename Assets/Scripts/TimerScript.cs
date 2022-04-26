using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    private int startTime = 0;
    public TextMeshProUGUI textMeshProUGUI;
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
        var time = (int)(Time.time * 1000) - startTime;
        return time;
    }

    public void StartTiming()
    {
        startTime = (int)(Time.time * 1000);
    }
}
