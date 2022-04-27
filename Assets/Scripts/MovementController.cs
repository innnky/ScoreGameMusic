using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bgs;
    public 
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        bgs.transform.Translate(0.03f,0,0);
        
    }
}
