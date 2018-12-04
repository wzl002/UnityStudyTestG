using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{

    Text HUDText;

    public float rotAngle = 0.0f;

    // Use this for initialization
    void Start()
    {
        HUDText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        HUDText.text = "Rotation: " + rotAngle.ToString("F1");
    }
}
