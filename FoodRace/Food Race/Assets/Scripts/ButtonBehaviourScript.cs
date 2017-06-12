using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviourScript : MonoBehaviour {
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(clickSound, Vector3.zero, GameVars.soundEffectVolume);
    }

    public void ZoomInButton()
    {
        if (transform.localScale.Equals(Vector3.one))
            transform.localScale += (new Vector3(.5f, .5f, 0));

    }
    public void ZoomOutButton()
    {
        if (transform.localScale.Equals(new Vector3(1.5f, 1.5f, 1f)))
            transform.localScale -= (new Vector3(.5f, .5f, 0));

    }
    
}
