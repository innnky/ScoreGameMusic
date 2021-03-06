using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetermineController : MonoBehaviour
{
    private List<Note> _notes = new List<Note>();
    private List<Note> _distinctNotes = new List<Note>();
    public float BPM;
    public int perfectRange;
    public int goodRange;
    private TimerScript _timerScript;
    public TextMeshProUGUI determineStatus;
    public Animator determineAnimator;
    public AudioController audioController;
    public UIController uiController;
    void Start()
    {
        _timerScript = GameObject.Find("TimeController").GetComponent<TimerScript>();
        determineStatus.text = "Perfect";
        TextAsset jsonText = Resources.Load("Notes") as TextAsset;
        JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonText.text);
        foreach (var jsonDataJnote in jsonData.jnotes)
        {
            _notes.Add(new Note(jsonDataJnote.key, jsonDataJnote.beat, BPM, perfectRange, goodRange, jsonDataJnote.isRest));
        }
        TextAsset jsonText2 = Resources.Load("NotesTimeDistinct") as TextAsset;
        JsonData jsonData2 = JsonUtility.FromJson<JsonData>(jsonText2.text);
        foreach (var jsonDataJnote2 in jsonData2.jnotes)
        {
            _distinctNotes.Add(new Note(jsonDataJnote2.key, jsonDataJnote2.beat, BPM, perfectRange, goodRange, jsonDataJnote2.isRest));
        }
        
    }

    void Update()
    {
        int t = _timerScript.GetTime();
        for (int i = 0; i < _notes.Count; i++)
        {
            if (!_notes[i].GetStatus() && t > _notes[i].GetMissTime())
            {
                _notes[i].SetComplete();
                if (_notes[i].GetKey()==0)
                {
                    audioController.PlayKick();
                }
                else if (_notes[i].GetKey() == 1)
                {
                    audioController.PlaySnare();
                }
                else if (_notes[i].GetKey()==2)
                {
                    audioController.PlayHihatClose();
                }
                else if (_notes[i].GetKey()==3)
                {
                    audioController.PlayHihatOpen();
                }
                else if (_notes[i].GetKey()==4)
                {
                    audioController.PlayCrash();
                }
                else if (_notes[i].GetKey()==7)
                {
                    audioController.PlayFloorTom();
                }
                // audioController
                MissTriggered();
            }
        }
    }

    public void KeyTriggered(int key)
    {
        int time = _timerScript.GetTime();
        foreach (Note note in _notes)
        {
            if (!note.GetStatus() && note.GetKey() == key)
            {
                if (note.IsPerfect(time))
                {
                    note.SetComplete();
                    PerfectTriggered();
                    return;
                }
                else if (note.IsGood(time))
                {
                    note.SetComplete();
                    GoodTriggered();
                    return;
                }
            }
        }
        
    }

    private void MissTriggered()
    {
        determineAnimator.Play("DetermineStatusAnimation");
        determineStatus.text = "Miss!!";
        uiController.clearCombo();
    }
    //PerfectTriggered
    private void PerfectTriggered()
    {
        determineAnimator.Play("DetermineStatusAnimation");
        determineStatus.text = "Perfect!!";
        uiController.addCombo();
        uiController.addScore(25);
    }
    
    //goodTriggered
    private void GoodTriggered()
    {
        determineAnimator.Play("DetermineStatusAnimation");
        determineStatus.text = "Good!!";
        uiController.addCombo();
        uiController.addScore(10);
    }
    //get _distinctNotes
    public List<Note> GetDistinctNotes()
    {
        return _distinctNotes;
    }
}

[Serializable]
public class JsonData  {
    public List<JsonNotes> jnotes;
}

[Serializable]
public class JsonNotes {
    public int key;
    public float beat;
    public bool isRest;
}
