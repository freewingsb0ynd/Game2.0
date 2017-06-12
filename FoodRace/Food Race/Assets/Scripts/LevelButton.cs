using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : Button {
	
	public string _thisLevel;
	public Text levelText;
	public int unlocked;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        TKSceneManager.ChangeScene(thisLevel);
    }

    public string thisLevel{
		get {return _thisLevel;}
		set { _thisLevel = value; }
	}
}
