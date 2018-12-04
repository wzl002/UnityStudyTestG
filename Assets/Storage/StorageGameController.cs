using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StorageGameController : MonoBehaviour {

	public static StorageGameController gCtrl;

	public int highScore = 0;
	public Font hsFont;

	public void Awake()
	{
		if (gCtrl == null) {
			DontDestroyOnLoad (gameObject);
			gCtrl = this;
			hsFont = Font.CreateDynamicFontFromOSFont ("Arial", 24);
		} else if (gCtrl != this) {
			gCtrl.LoadScore ();
			Destroy (gameObject);
		}
	}

	public void Start()
	{
		LoadScore ();
	}

	public void OnGUI()
	{
		Scene curScene = SceneManager.GetActiveScene ();
		if (curScene.name == "Main menu") {
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			GUIStyle tStyle = GUI.skin.GetStyle ("label");
			tStyle.alignment = TextAnchor.MiddleCenter;
			tStyle.font = gCtrl.hsFont;
			tStyle.richText = true;
			GUIContent tContent = new GUIContent("<color=yellow><b>High scores:</b>\n\n" + gCtrl.highScore + "</color>");
			Vector2 tSize = tStyle.CalcScreenSize(tStyle.CalcSize (tContent));
			GUI.Label (new Rect (x, y, tSize.x, tSize.y), tContent, tStyle);
			PlayerPrefs.SetString ("CurrentScore", "0");
		}
	}

	public void StartLevel()
	{
		PlayerPrefs.SetString ("Shape", "Cube");
		LoadScore ();
	}

	public void LoadTheLevel(string theLevel)
	{
		SceneManager.LoadScene (theLevel);
	}

	public void LoadScore()
	{
		if (File.Exists (Application.persistentDataPath + "/highscore.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.Open, FileAccess.Read);
			GameData data = (GameData)bf.Deserialize (fs);
			fs.Close ();
			gCtrl.highScore = data.score;
		}
	}

	public void SaveScore(int score)
	{
		if (score > gCtrl.highScore) {
			gCtrl.highScore = score;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.OpenOrCreate);
			GameData data = new GameData ();
			data.score = score;
			bf.Serialize (fs, data);
			fs.Close ();
		}
	}
}