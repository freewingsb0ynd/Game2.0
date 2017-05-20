using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1ButtonScript : MonoBehaviour
{
    public void Begin()
    {
        TKSceneManager.ChangeScene("Level2");
    }



}