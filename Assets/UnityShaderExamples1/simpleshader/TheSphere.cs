using UnityEngine;
using System.Collections;

public class TheSphere : MonoBehaviour {

	private float incr;

	// Use this for initialization
	void Start () {
		incr = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		Renderer rend = GetComponent<Renderer> ();
		Vector4 col = rend.material.GetVector ("_Color");
		col.x += incr;
		if ((col.x > 1) || (col.x < 0))
			incr = -incr;
		rend.material.SetVector ("_Color", col);
	}
}
