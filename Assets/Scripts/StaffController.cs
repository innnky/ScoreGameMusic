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

    public GameObject staffParent;

    public GameObject referenceImage;
    
    
    public float actualWidth;
    public float actualHeight;
    public float actualStaffWidth;  
    //private
    private GameObject currentLine;
    private List<NotePos> notePosList;
    private StaffImageManager staffImageManager;
    public Vector2 staffOffset;
    // public Vector2 allStaffOffset;
    public MovementController movementController;
    
    void Start()
    {
        initCurrentLine();
        initNotePos();
        staffImageManager = new StaffImageManager(staffParent, notePosList,
            determineController.GetDistinctNotes(), determineController.BPM, timerScript,this);
        // allStaffOffset = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (timerScript.getPaused())
        {
            return;
        }
        staffImageManager.update();

        float currentPositon = staffImageManager.getCurrentPositon();
        currentLine.transform.position = new Vector3(currentLineStartX+currentPositon, currentLineStartY, 0);
    }
    

    private void initCurrentLine()
    {
        currentLine = Instantiate(currentLinePrefab, new Vector3(currentLineStartX, currentLineStartY, 0), Quaternion.identity);
    }

    private void initNotePos()
    {
        SpriteRenderer spriteRenderer = referenceImage.GetComponent<SpriteRenderer>();
        actualWidth = spriteRenderer.sprite.bounds.size.x;
        actualHeight = spriteRenderer.sprite.bounds.size.y;
        TextAsset jsonText = Resources.Load("NotesPos") as TextAsset;
        PosData posData = JsonUtility.FromJson<PosData>(jsonText.text);
        notePosList = new List<NotePos>();
        int lineCount = -1;
        int lastY = -1;
        int lineStartX = -1;
        foreach (NotePosJson notePos in posData.positions)
        {
            if (lastY!=notePos.y)
            {
                lineCount++;
                lineStartX = notePos.x;
            }
            var pos = new NotePos(notePos);
            pos.setStaffWidth(staffWidth);
            pos.setActualWidth(actualWidth);
            pos.setActualHeight(actualHeight);
            pos.setActualStaffWidth(actualStaffWidth);
            pos.setLine(lineCount);
            pos.setScale(scale);
            pos.setRealX(lineCount * (actualStaffWidth - lineStartX * actualWidth / 35724f)+pos.getActualX());
            notePosList.Add(pos);
            lastY = notePos.y;
        }
        
    }
    
    //get currentLine
    public GameObject getCurrentLine()
    {
        return currentLine;
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
