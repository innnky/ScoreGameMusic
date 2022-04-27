
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaffImageManager
{
    private GameObject staffParent;
    private List<GameObject> staffList;
    private List<NotePos> notePositions;
    private List<Note> notes;
    private int noteNum;
    private float BPM;
    private TimerScript timer;
    private int currentPointer = 0;
    private int preNum = 20;
    private int newestY = 0;
    private StaffController staffController;
    public StaffImageManager( GameObject staffParent,
        List<NotePos> notePositions,
        List<Note> notes, float BPM, TimerScript timer, StaffController staffController)
    {
        this.staffParent = staffParent;
        this.staffList = new List<GameObject>();
        this.notePositions = notePositions;
        this.notes = notes;
        this.noteNum = notes.Count;
        this.BPM = BPM;
        this.timer = timer;
        newestY = notePositions[0].getY();
        this.staffController = staffController;
        beatPerBar = staffController.beatsPerBar;
        firstLoadImageFromResource();
        initImage();
        initBars();
    }

    

    public void update()
    {
        updateCurrentBeat();
        updateImage();
        updateBars();
    }

    private float getCurrentBeat()
    {
        int time = timer.GetTime();
        float timePerBeat = 60000 / BPM;
        return time / timePerBeat;
    }

    public void updateCurrentBeat()
    {
        try
        {
            float currentBeat = getCurrentBeat();
            if (notes[currentPointer+1].GetBeat()<=currentBeat)
            {
                currentPointer++;
            }
            if(currentBeat < notes[currentPointer].GetBeat())
            {
                throw new System.Exception("current beat is less than note beat");
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            currentPointer = noteNum - 1;
        }
        
    }

    private Vector3 lastPos;
    public void updateImage()
    {
        try
        {
            if (notePositions[currentPointer+preNum].getY()!=newestY)
            {
                loadImage(notePositions[currentPointer+preNum], notePositions[currentPointer+preNum-1]);
            }
            newestY = notePositions[currentPointer+preNum].getY();
        }
        catch (ArgumentOutOfRangeException e)
        {
        }
        
    }

    private void loadImage(NotePos newLineNote, NotePos oldLineNote)
    {
        Vector3 newPos = new Vector3(lastPos.x + staffController.actualStaffWidth - newLineNote.getActualX(),
            lastPos.y + newLineNote.getActualY() - oldLineNote.getActualY(), 0);
        
        GameObject staff = createImage(newPos);
        staffList.Add(staff);
        lastPos = newPos;
        staff.transform.SetParent(staffParent.transform);
        if (staffList.Count>3)
        {
            GameObject.Destroy(staffList[0]);
            staffList.RemoveAt(0);
        }
        
    }
    private void initImage()
    {
        lastPos = new Vector3(staffController.staffOffset.x, staffController.staffOffset.y, 0);
        GameObject staff = createImage(lastPos);
        staff.transform.SetParent(staffParent.transform);
        staffList.Add(staff);
    }

    GameObject testImg = new GameObject();
    private void firstLoadImageFromResource()
    {
        SpriteRenderer spriteRenderer = testImg.AddComponent<SpriteRenderer>();
        Texture2D tex = Resources.Load("ms-1") as Texture2D;
        var sprite = Sprite.Create(tex, new Rect(0,0, tex.width,tex.height), new Vector2(0, 0));
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerName="Top";
        testImg.transform.localScale = new Vector3(2.0546875f, 2.0546875f);
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    private GameObject createImage(Vector3 pos)
    {
        return GameObject.Instantiate(testImg, pos, Quaternion.identity);
       
    }

    public float getCurrentPositon()
    {
        float beat = getCurrentBeat();
        try
        {
            float beatDecimal =(beat - notes[currentPointer].GetBeat())/(notes[currentPointer+1].GetBeat()-notes[currentPointer].GetBeat());
            //计算出当前的位置
            float interval = notePositions[currentPointer+1].getRealX() - notePositions[currentPointer].getRealX();
            float currentPosition = notePositions[currentPointer].getRealX() + interval * beatDecimal;
            return currentPosition;
        }
        catch (ArgumentOutOfRangeException e)
        {
            return notePositions[currentPointer].getRealX();
        }
        
    }


    private List<int> barStartIndexes;
    private int beatPerBar;
    private int lastBar;
    
    private int calcBar(float beat)
    {
        //通过beat计算出当前的bar
        int bar = (int)(beat / beatPerBar);
        return bar;
    }

    private void initBars()
    {
        barStartIndexes = new List<int>();
        int maxBar = calcBar(notes[notes.Count - 1].GetBeat());
        int newestBar = -1;
        for (var i = 0; i < notes.Count; i++)
        {
            Note note = notes[i];
            if(calcBar(note.GetBeat())!=newestBar)
            {
                barStartIndexes.Add(i);
                newestBar = calcBar(note.GetBeat());
            }
        }
    }

    private float getBarPos(int bar)
    {
        try
        {
            return notePositions[barStartIndexes[bar]].getRealX();

        }
        catch (ArgumentOutOfRangeException e)
        {
            return notePositions[notePositions.Count - 1].getRealX();
        }
        
    }
    private bool isInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面
 
        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) return true;
        else return false;
    }

    public bool needMove()
    {
        try
        {
            float barPos = getBarPos(calcBar(getCurrentBeat())+2);
            if (isInView(new Vector3(staffController.currentLineStartX+barPos, staffController.currentLineStartY, 0))) 
                return false;
            else 
                return true;
        }
        catch (ArgumentOutOfRangeException e)
        {
            return true;
        }
        
    }

    private void updateBars()
    {
        int currentBar = calcBar(getCurrentBeat());

        if (lastBar !=currentBar)
        {
            if (needMove())
            {
                staffController.movementController.MoveToRealX(getBarPos(currentBar));
            }           
        }

        lastBar = currentBar;
    }
    
    






}
