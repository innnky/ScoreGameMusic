using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    private readonly int key;
    private readonly float beat;
    private int perfectTime;
    private readonly float BPM;
    private readonly int perfectRange;
    private readonly int goodRange;
    private bool status = false;
    public Note(int key, float beat, float bpm, int perfectRange, int goodRange)
    {
        this.key = key;
        this.beat = beat;
        BPM = bpm;
        this.perfectRange = perfectRange;
        this.goodRange = goodRange;
        CalcPerfectTime();
    }
//100b / 60*1000
    private void CalcPerfectTime()
    {
        perfectTime = (int)(60000 * beat / BPM);
    }

    public int GetKey()
    {
        return key;
    }
    public float GetBeat()
    {
        return beat;
    }
    public int GetPerfectTime()
    {
        return perfectTime;
    }
    
    // getStatus
    public bool GetStatus()
    {
        return status;
    }
    //setCompleted
    public void SetComplete()
    {
        this.status = true;
    }
    
    //getMissTime
    public int GetMissTime()
    {
        return perfectTime + goodRange;
    }
    //isPerfect
    public bool IsPerfect(int time)
    {
        return time <= perfectTime + perfectRange && time >= perfectTime - perfectRange;
    }
    //isGood
    public bool IsGood(int time)
    {
        return time <= perfectTime + goodRange && time >= perfectTime - goodRange;
    }
    
    
}
