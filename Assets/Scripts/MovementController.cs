using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bgs;
    // public StaffController staffController;
    public int xOffset;
    private Vector3 currentPos;
    private float lastX;
    void Start()
    {
        lastX = xOffset;
    }

    void FixedUpdate()
    {
        // bgs.transform.Translate(0.03f,0,0);
        
    }
    
    public void MoveToRealX(float x)
    {
        
        bgs.transform.Translate(x-lastX,0,0);
        lastX = x;
    }
}
