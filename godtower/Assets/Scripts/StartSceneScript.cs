using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour {
    public void Begin(){
        TKSceneManager.ChangeScene("Level1");
    }
    
}
