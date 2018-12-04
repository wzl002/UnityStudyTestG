using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	public void LoadTheLevel(string theLevel)
	{
		SceneManager.LoadScene (theLevel);
		if (theLevel.StartsWith ("Level")) {
			string levelStr = PlayerPrefs.GetString ("LastLevel");
			int lastLevel = System.Convert.ToInt32 (levelStr);
			if (lastLevel >= 0)
				GameController.gCtrl.PlayLevelMusic (lastLevel);
		} else
			GameController.gCtrl.StopAllMusic ();
	}
}
