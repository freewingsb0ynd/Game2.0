using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour {
    public Action<float> OnMovePressed;

    public Action OnJumpPressed;
    public Action OnDashPressed;

    public KeyCode jumpButton;
    public KeyCode dashButton;

    public Vector2 Direction { get; private set; }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
        if (OnMovePressed != null)
        {
            OnMovePressed(Input.GetAxisRaw("Horizontal"));
        }

        if (OnJumpPressed != null && Input.GetKeyDown(jumpButton))
        {
            OnJumpPressed();
        }

        if (OnDashPressed != null && Input.GetKeyDown(dashButton))
        {
            OnDashPressed();
        }

    }
    
}
