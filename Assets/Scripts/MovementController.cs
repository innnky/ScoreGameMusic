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
    private Vector3 target;
    private bool flag = false;
    void Start()
    {
        lastX = xOffset;
        
    }

    void FixedUpdate()
    {
        if (flag)
        {
            bgs.transform.position = Vector3.Lerp(bgs.transform.position, target, 0.3f);
        }
    }
    
    public void MoveToRealX(float x)
    {
        target = new Vector3(bgs.transform.position.x +x-lastX, bgs.transform.position.y, bgs.transform.position.z);
        lastX = x;
        flag = true;
    }
}
