using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
    
	private GameObject lvlButt;
	public GameObject levelPanel;
	private bool setting = false;
	private bool level = false;
	public LevelButton[] levelButtons;
	public string nextLevel;
	public int levelToUnlock;
    public Text totalStarsText;

    private int totalStars;
	// Use this for initialization
	void Start () {
        
		setting = false;
		level = true;
        //		PlayerPrefs.DeleteAll ();

        totalStars = 0;
		int levelReached = PlayerPrefs.HasKey("levelReached") ? PlayerPrefs.GetInt ("levelReached") : 1;
		Debug.Log (levelReached);
		for (int i = 0; i < levelButtons.Length; i++) {
            int level = i + 1;
            levelButtons[i].thisLevel = "Level " + level;

			if (i + 1 > levelReached) {
				levelButtons [i].interactable = false;
			}
            
            if(levelButtons[i].interactable)
            {
                string stringToLoadStars;
                stringToLoadStars = "starMaxReachedLevel" + level;
                int starReached = PlayerPrefs.GetInt(stringToLoadStars,0);
                totalStars += starReached;
                ShowStars(starReached, levelButtons[i]);
            }
		}

        totalStarsText.text = ""+totalStars;


    }

    void ShowStars(int stars, LevelButton levelButton)
    {
        if (stars > 0) levelButton.star1.SetActive(true);            
        if (stars > 1) levelButton.star2.SetActive(true);
        if (stars > 2) levelButton.star3.SetActive(true);
    }


    // Update is called once per frame
    void Update () {
		
	}

	//Add Level to the scene
    


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

	public void WinLevel(){
		
		PlayerPrefs.SetInt ("levelReached", levelToUnlock);

	}
}
