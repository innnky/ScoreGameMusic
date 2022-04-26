using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetermineController : MonoBehaviour
{
    private List<Note> _notes = new List<Note>();
    public float BPM;
    public int perfectRange;
    public int goodRange;
    private TimerScript _timerScript;
    public TextMeshProUGUI determineStatus;
    public Animator determineAnimator;

    void Start()
    {
        for (int i = 2; i < 10; i++)
        {
            _notes.Add(new Note(0, i, BPM, perfectRange, goodRange));
        }
        _timerScript = GameObject.Find("TimeController").GetComponent<TimerScript>();
        determineStatus.text = "Perfect";
    }

    void Update()
    {
        int t = _timerScript.GetTime();
        for (int i = 0; i < _notes.Count; i++)
        {
            if (!_notes[i].GetStatus() && t > _notes[i].GetMissTime())
            {
                _notes[i].SetComplete();
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
    }
    //PerfectTriggered
    private void PerfectTriggered()
    {
        determineAnimator.Play("DetermineStatusAnimation");
        determineStatus.text = "Perfect!!";
    }
    
    //goodTriggered
    private void GoodTriggered()
    {
        determineAnimator.Play("DetermineStatusAnimation");
        determineStatus.text = "Good!!";
    }
}
