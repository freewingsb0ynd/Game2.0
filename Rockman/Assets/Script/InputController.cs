using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public Vector2 Direction { get; private set; }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
        Direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        
    }
}
