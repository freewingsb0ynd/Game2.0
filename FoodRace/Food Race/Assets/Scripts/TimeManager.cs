using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public static float startingTime;
	public bool time= false;
	public bool setting = false;
	private Text timeText;
	// Use this for initialization
	void Start () {
		time = true;
		setting = false;
		timeText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (time) {
			startingTime -= Time.deltaTime;
			if (startingTime <= 0) {
				startingTime = 0;
				GameManager.Instance.end = true;
				GameManager.Instance.GameOver ();
			}
			timeText.text = "" + Mathf.Round (startingTime);
		}
	}

	public void Setting (){
		setting = !setting;
		if (setting)
			time = false;
		else if (!setting)
			time = true;
			
	}
}
