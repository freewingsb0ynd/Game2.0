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
    public Button hintButton;
    public GameObject hintPanel;
    public GameObject mainPanel;

    private bool isHintShown = false;
    private bool isMainShown = true;



    // Update is called once per frame
    void Update () {
        if (Time.timeSinceLevelLoad % 2 < 1) levelText.text = "LEVEL";
        else levelText.text = lvlNumber;
        if (passwordInput.isFocused && Input.GetKey(KeyCode.Return)){
            Submit();
        }
    }
    
    public void Submit() {
        if (passwordInput.text == password) TKSceneManager.ChangeScene(nextSceneName);
        else deniedText.gameObject.SetActive(true); 
    }

    public void ShowHintPanel(){
        if(!isHintShown) hintPanel.gameObject.SetActive(true);
        else hintPanel.gameObject.SetActive(false);
        isHintShown = !isHintShown;
    }

    public void UnShowMainPanel(){
        if (!isMainShown) mainPanel.gameObject.SetActive(true);
        else mainPanel.gameObject.SetActive(false);
        isMainShown = !isMainShown;
    }

}
