
using System.Collections.Generic;
using UnityEngine;

public class StaffImageManager
{
    private GameObject image;
    private GameObject staffParent;
    private List<GameObject> staffList;
    private List<NotePos> notePositions;
    private List<Note> notes;
    private int noteNum;
    private float BPM;
    private TimerScript timer;
    private int currentPointer = 0;
    private int preNum = 10;
    private int newestY = 0;
    private StaffController staffController;
    public StaffImageManager(GameObject image, GameObject staffParent,
        List<NotePos> notePositions,
        List<Note> notes, float BPM, TimerScript timer, StaffController staffController)
    {
        this.image = image;
        this.staffParent = staffParent;
        this.staffList = new List<GameObject>();
        this.notePositions = notePositions;
        this.notes = notes;
        this.noteNum = notes.Count;
        this.BPM = BPM;
        this.timer = timer;
        newestY = notePositions[0].getY();
        this.staffController = staffController;
        initImage();
    }

    

    public void update()
    {
        updateCurrentBeat();
        updateImage();
    }

    private float getCurrentBeat()
    {
        int time = timer.GetTime();
        float timePerBeat = 60000 / BPM;
        return time / timePerBeat;
    }

    public void updateCurrentBeat()
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

    private Vector3 lastPos;
    public void updateImage()
    {
        if (notePositions[currentPointer+preNum].getY()!=newestY)
        {
            loadImage(notePositions[currentPointer+preNum], notePositions[currentPointer+preNum-1]);
        }
        newestY = notePositions[currentPointer+preNum].getY();
    }

    private void loadImage(NotePos newLineNote, NotePos oldLineNote)
    {
        Vector3 newPos = new Vector3(lastPos.x + staffController.actualStaffWidth - newLineNote.getActualX(),
            lastPos.y + newLineNote.getActualY() - oldLineNote.getActualY(), 0);
        GameObject staff = GameObject.Instantiate(image, newPos, Quaternion.identity);
        staffList.Add(staff);
        lastPos = newPos;
        staff.transform.SetParent(staffParent.transform);
        if (staffList.Count>2)
        {
            GameObject.Destroy(staffList[0]);
            staffList.RemoveAt(0);
        }
        
    }
    private void initImage()
    {
        lastPos = new Vector3(staffController.staffOffset.x, staffController.staffOffset.y, 0);
        GameObject staff = GameObject.Instantiate(image, lastPos, Quaternion.identity);
        staff.transform.SetParent(staffParent.transform);
        staffList.Add(staff);
    }

    public float getCurrentPositon()
    {
        float beat = getCurrentBeat();
        float beatDecimal =(beat - notes[currentPointer].GetBeat())/(notes[currentPointer+1].GetBeat()-notes[currentPointer].GetBeat());
        //计算出当前的位置
        float interval = notePositions[currentPointer+1].getRealX() - notePositions[currentPointer].getRealX();
       float currentPosition = notePositions[currentPointer].getRealX() + interval * beatDecimal;
       return currentPosition;
    }

    
    
    
    
    
    
    
    
}
