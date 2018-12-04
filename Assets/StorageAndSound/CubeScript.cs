using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour {

	public float rotationSpeed = 1.0f;
	public GameObject hud;
	public GameObject otherShape;
	private GameData gameData;
	private int score = 0;
	private Text HUDText;

	// Use this for initialization
	void Start () {
		string scoreStr = PlayerPrefs.GetString ("CurrentScore");
		score = System.Convert.ToInt32 (scoreStr);
		HUDText = hud.GetComponent<Text> ();
		HUDText.text = "Score: " + score;
		string shapeName = PlayerPrefs.GetString ("Shape");
		if (shapeName != gameObject.name) {
			otherShape.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotationSpeed, 0, rotationSpeed);
		score++;
		HUDText.text = "Score: " + score;
		bool spacePressed = Input.GetKeyDown (KeyCode.Space);
		bool singleTap = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began;
		bool changeShape = Input.GetKeyDown (KeyCode.S);
		bool doubleTap = Input.touchCount > 1 && Input.GetTouch (0).phase == TouchPhase.Began;
		bool playSFX = Input.GetKeyDown (KeyCode.X);
		if (spacePressed || singleTap) {
			PlayerPrefs.SetString ("CurrentScore", score.ToString());
			GameController.gCtrl.SaveScore (score);
			PlayerPrefs.SetString ("LastLevel", "0");
			GameController.gCtrl.PauseLevelMusic (0);
			GameController.gCtrl.LoadTheLevel ("Pause Screen");
		}
		if (changeShape || doubleTap) {
			otherShape.SetActive (true);
			gameObject.SetActive (false);
			PlayerPrefs.SetString ("Shape", otherShape.name);
		}
		if (playSFX)
			GameController.gCtrl.PlaySFX ();
	
	}
}
