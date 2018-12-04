using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float speed = 10f;
    private bool isRotating = true;
    private Vector3 rvec = Vector3.one;
    
    // Use this for initialization
    void Start()
    {
        rvec.Set(1, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {

        if (isRotating)
        {
            float rotAmount = speed * Time.deltaTime;
            transform.Rotate(rvec, rotAmount);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = !isRotating;
        }
    }
}

