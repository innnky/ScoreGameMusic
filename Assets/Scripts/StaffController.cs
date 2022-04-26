using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    
    public float currentLineStartX;
    public float currentLineStartY;
    
    public GameObject currentLinePrefab;


    public int beatsPerBar;
    public TimerScript timerScript;
    public DetermineController determineController;
    

    
    //private
    private GameObject currentLine;
    private List<NotePos> notePosList;

    void Start()
    {
        initCurrentLine();
        TextAsset jsonText = Resources.Load("NotesPos") as TextAsset;
        PosData posData = JsonUtility.FromJson<PosData>(jsonText.text);
        notePosList = posData.positions;
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


    }
    
    float fixInterval = 0.85f;
    float fixNoteStartInterval = 0.85f;
    

    private void initCurrentLine()
    {
        currentLine = Instantiate(currentLinePrefab, new Vector3(currentLineStartX, currentLineStartY, 0), Quaternion.identity);
    }
    
    
}

[Serializable]
public class PosData  {
    public List<NotePos> positions;
}

[Serializable]
public class NotePos {
    public int x;
    public int y;
}
