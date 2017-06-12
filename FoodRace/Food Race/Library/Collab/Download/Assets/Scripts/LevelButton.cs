using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
	public static LevelButton Instance {
		get;
		private set;
	}
	public string _thisLevel;
	public Text levelText;
	public int unlocked;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	void Start(){
		Instance = this;

	}

	public string thisLevel{
		get {return _thisLevel;}
		set { _thisLevel = value; }
	}
}
