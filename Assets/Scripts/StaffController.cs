using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    
    public float currentLineStartX;
    public float currentLineStartY;
    public float staffWidth;
    public int scale;
    public GameObject currentLinePrefab;
    

    public int beatsPerBar;
    public TimerScript timerScript;
    public DetermineController determineController;

    public GameObject image;
    public GameObject mask;
    public GameObject staffParent;
    
    
    //private
    private GameObject currentLine;
    private List<NotePos> notePosList;
    private StaffImageManager staffImageManager;
    
    void Start()
    {
        initCurrentLine();
        initNotePos();
        staffImageManager = new StaffImageManager(image, staffParent, notePosList,
            determineController.GetDistinctNotes(), determineController.BPM, timerScript,this);
    }

    void FixedUpdate()
    {
        staffImageManager.update();
        float currentPositon = staffImageManager.getCurrentPositon();
        currentLine.transform.position = new Vector3(currentLineStartX+currentPositon/scale, currentLineStartY, 0);
    }
    
    float fixInterval = 0.85f;
    float fixNoteStartInterval = 0.85f;
    

    private void initCurrentLine()
    {
        currentLine = Instantiate(currentLinePrefab, new Vector3(currentLineStartX, currentLineStartY, 0), Quaternion.identity);
    }

    private void initNotePos()
    {
        TextAsset jsonText = Resources.Load("NotesPos") as TextAsset;
        PosData posData = JsonUtility.FromJson<PosData>(jsonText.text);
        notePosList = new List<NotePos>();
        int lineCount = 0;
        int lastY = posData.positions[0].y;
        foreach (NotePosJson notePos in posData.positions)
        {
            if (lastY!=notePos.y)
            {
                lineCount++;
            }
            var pos = new NotePos(notePos);
            pos.setStaffWidth(staffWidth);
            pos.setLine(lineCount);
            pos.setScale(scale);
            notePosList.Add(pos);
            lastY = notePos.y;
        }
        
    }
    
    
}

[Serializable]
public class PosData  {
    public List<NotePosJson> positions;
}

[Serializable]
public class NotePosJson {
    public int x;
    public int y;
}
