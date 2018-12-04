using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	public void LoadTheLevel(string theLevel)
	{
		SceneManager.LoadScene (theLevel);
	}
}
