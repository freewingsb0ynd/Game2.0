using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehaviourScript : MonoBehaviour {
    private Image barImage;
    // Use this for initialization
    void Start () {
        barImage = gameObject.GetComponent<Image>();
    }

    public void HideBar() {
        Color barImageColor = barImage.color;
        barImageColor.a = 0;
        barImage.color = barImageColor;
    }

    public void ShowBar() {
        Color barImageColor = barImage.color;
        barImageColor.a = 1;
        barImage.color = barImageColor;

    }
}
