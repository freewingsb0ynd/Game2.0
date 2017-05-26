using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviourScript : MonoBehaviour {
    private Transform buttonRectTransform;

    // Use this for initialization
    void Start(){
        buttonRectTransform = gameObject.GetComponent<Transform>();
    }
    
    public void rotateButton(){
        if (buttonRectTransform.localRotation.z == -180) buttonRectTransform.Rotate(new Vector3(0, 0, 0));
        else buttonRectTransform.Rotate(new Vector3(0, 0, -180));
    }
    public void resizeButton(){
        if (buttonRectTransform.localScale.Equals(new Vector3(1, 1, 1)))
            buttonRectTransform.localScale += (new Vector3(.5f, .5f, 0));
        else buttonRectTransform.localScale -= (new Vector3(.5f, .5f, 0));
    }
}
