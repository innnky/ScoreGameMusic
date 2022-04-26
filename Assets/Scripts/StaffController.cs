using System.Collections;
using System.Collections.Generic;
using pojo;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    public float barLineStartX;
    public float barLineStartY;
    
    public float noteStartX;
    public float kickStartY;
    
    public float currentLineStartX;
    public float currentLineStartY;
    
    public GameObject barLinePrefab;
    public GameObject notes;
    public GameObject kickPrefab;
    public GameObject currentLinePrefab;


    public int beatsPerBar;
    public TimerScript timerScript;
    public DetermineController determineController;
    
    List<List<BeatPoint>> beatPoints = new List<List<BeatPoint>>();

    
    //private
    private GameObject currentLine;
    
    
    void Start()
    {
        initializeBeatPoints();
        initKicks();
        initCurrentLine();
        
    }

    void FixedUpdate()
    {
        int time = timerScript.GetTime();
        float BPM = determineController.BPM;
        int timePerBeat = (int)(60000 / BPM);
        float currentBeat = time / (float)timePerBeat;
        float currentBeatDecimal = currentBeat - (int)currentBeat;
        int currentBeatInt = (int)currentBeat;
        int currentBar = currentBeatInt / beatsPerBar;
        int currentBeatInBar = currentBeatInt % beatsPerBar;
        // beatPoints[currentBar][currentBeatInBar].Offset
        float stx =beatPoints[currentBar][currentBeatInBar].RelativeStart;
        float len =  beatPoints[currentBar][currentBeatInBar].Length;
        float cx = stx + len * currentBeatDecimal;
        currentLine.transform.position = new Vector3(currentLineStartX + cx, currentLineStartY, 0);
        
        
    }

    private void initializeBeatPoints()
    {
        //初始化二维数组
        for (int i = 0; i < 4; i++)
        {
            beatPoints.Add(new List<BeatPoint>());
            for (int j = 0; j < beatsPerBar; j++)
            {
                beatPoints[i].Add(new BeatPoint(i*4+j, determineController.BPM,(float)(0.85*j),beatsPerBar, 0.85f));
            }
        }
    }
    float fixInterval = 0.85f;
    float fixNoteStartInterval = 0.85f;

    private void initKicks()
    {
        float accumulatedStart = 0f;
        //遍历BeatPoints
        for (int i = 0; i < beatPoints.Count; i++)
        {
            float thisBarLength = 0;
            float lastBeatLength = 0;
            for (int j = 0; j < beatPoints[i].Count; j++)
            {
                thisBarLength = beatPoints[i][j].Offset;
                lastBeatLength = beatPoints[i][j].Length;
                beatPoints[i][j].RelativeStart =
                    fixNoteStartInterval  + accumulatedStart + beatPoints[i][j].Offset;
                GameObject note = Instantiate(kickPrefab, new Vector3(beatPoints[i][j].RelativeStart+ noteStartX, kickStartY, 0), Quaternion.identity);
                note.transform.parent = notes.transform;
                beatPoints[i][j].BarStartOffset = accumulatedStart;
            }

            accumulatedStart = lastBeatLength + thisBarLength + accumulatedStart;
            // Debug.Log(""+accumulatedStart);

            GameObject barLine = Instantiate(barLinePrefab, new Vector3(barLineStartX+accumulatedStart+lastBeatLength/2 , barLineStartY, 0), Quaternion.identity);
            barLine.transform.parent = notes.transform;
        }

    }

    private void initCurrentLine()
    {
        currentLine = Instantiate(currentLinePrefab, new Vector3(currentLineStartX, currentLineStartY, 0), Quaternion.identity);
        currentLine.transform.parent = notes.transform;
    }

    private float getBarStart(int bar)
    {
        return beatPoints[bar][0].BarStartOffset;
    }
    private float getBarEnd(int bar)
    {
        return beatPoints[bar][0].BarStartOffset + beatPoints[bar][beatPoints[bar].Count-1].Offset + fixInterval+fixNoteStartInterval;
    }
}
