using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public string lvlNumber;
    public string password;
    public string nextSceneName;
    public Text levelText;
    public InputField passwordInput;
    public Button submitButton;
    public Text deniedText;
    public Button hintButton;
    public List<GameObject> hints;

    private int currentHintIndex;
    private void Start(){
        if (hints.Count > 1) hintButton.gameObject.SetActive(true); 
    }

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

    public void OnHintButtonClicked() {
        currentHintIndex = (currentHintIndex + 1) % hints.Count;

        if (currentHintIndex == hints.Count - 1 || currentHintIndex == 0) hintButton.GetComponent<Transform>().Rotate(new Vector3(0, 0, -180));

        for (int i =0; i < hints.Count; i++){
            if (i == currentHintIndex) hints[i].gameObject.SetActive(true);
            else hints[i].gameObject.SetActive(false);
        }
    }
}
