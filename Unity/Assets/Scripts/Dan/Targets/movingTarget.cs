using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTarget : MonoBehaviour
{
    public float moveSpeed = 10;
    [SerializeField]
    private bool movingLeft;

    public float minPos;
    public float maxPos;
    void Start() 
    {
        movingLeft = true;
        
       
    }
    void Update() 
    {
        
        if (movingLeft == true) 
        {
            // move left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= minPos) 
            {
                movingLeft = false;
            }
        } 
        else 
        {
            // move right
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= maxPos) 
            {
                movingLeft = true;
            }
        }
    }
}