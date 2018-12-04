using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CubeRotate : MonoBehaviour {

	public float rotationSpeed = 1.0f;
	private int count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotationSpeed, 0, rotationSpeed);
		count++;
		if (count > 100) {
			SceneManager.LoadScene ("Game Over");
		}
	}
}
