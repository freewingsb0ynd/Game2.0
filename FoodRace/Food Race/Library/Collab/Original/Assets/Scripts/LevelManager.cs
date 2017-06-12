﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
	
	public GameObject levelPanel;
	private bool setting = false;
	private bool level = false;
	public Button[] levelButtons;
	public string nextLevel;
	public int levelToUnlock;
	// Use this for initialization
	void Start () {
		setting = false;
		level = true;
		int levelReached = PlayerPrefs.GetInt ("levelReached", 1);
		for (int i = 0; i < levelButtons.Length; i++) {
			if (i + 1 > levelReached) {
				levelButtons [i].interactable = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Add Level to the scene

	public void OnClick(){
		TKSceneManager.ChangeScene ("Level 1");
	}


	public void Level(){
		PlayerPrefs.SetInt ("levelReached", levelToUnlock);
	}

	public void SettingPanel(){
		level = false;
		setting = true;
		levelPanel.SetActive(false);

	}

	public void LevelPanel(){
		level = true;
		setting = false;
		levelPanel.SetActive (true);
	}

	public void ToTitle(){
		TKSceneManager.ChangeScene ("Start Scene");
	}

}
