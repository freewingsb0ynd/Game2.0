using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviourScript : MonoBehaviour {
   
    public void resizeButton(){
        if (transform.localScale.Equals(new Vector3(1, 1, 1)))
            transform.localScale += (new Vector3(.5f, .5f, 0));
        else transform.localScale -= (new Vector3(.5f, .5f, 0));
    }
}
