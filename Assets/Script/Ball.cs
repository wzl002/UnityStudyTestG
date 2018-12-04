using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float thrust;
    private Rigidbody rb;

    //public GameObject fallingBall;
    private Quaternion defaultRotation;
    private Vector3 defaultBallPosition, defaultBallVelocity,
    defaultBallAngularVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -thrust);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("r"))
        {
            //fallingBall.SetActive(true);
            transform.rotation = defaultRotation;
            rb.transform.position = defaultBallPosition;
            rb.velocity = defaultBallVelocity;
            rb.angularVelocity = defaultBallAngularVelocity;
        }
        if (Input.GetKeyDown("="))
        {
            transform.Rotate(new Vector3(0, 0, 1), -2);
        }
        else if (Input.GetKeyDown("-"))
        {
            transform.Rotate(new Vector3(0, 0, 1), 2);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * thrust);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ball")
        {
            col.gameObject.SetActive(false);
        }
    }

}
