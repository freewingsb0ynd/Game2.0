using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed;

    private InputController inputController;
    private Vector2 velocity;


    private void Awake()
    {
        inputController = GetComponent<InputController>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        velocity = new Vector2(inputController.Direction.x * movementSpeed, velocity.y + Physics2D.gravity.y + Time.fixedDeltaTime);

        transform.position += (Vector3)(velocity * Time.fixedDeltaTime);
    }
}
