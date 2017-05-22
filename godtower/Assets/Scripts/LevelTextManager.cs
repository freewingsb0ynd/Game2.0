using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTextManager : MonoBehaviour {
    public string lvlNumber;
    public string password;
    public string nextSceneName;
    
    public Text levelText;
    public InputField passwordInput;
    public Button submitButton;
    public Text deniedText;

	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad % 2 < 1) levelText.text = "LEVEL";
        else levelText.text = lvlNumber;
    }

    public void Submit() {
        if (passwordInput.text == password) TKSceneManager.ChangeScene(nextSceneName);
        else deniedText.gameObject.SetActive(true); 
    }
}
