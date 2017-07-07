using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed;

    private InputController inputController;
    private Vector2 velocity;
    private Controller2D controller2D;
    private PlayerStatus playerStatus;


    private void Awake()
    {
        inputController = GetComponent<InputController>();
        controller2D = GetComponent<Controller2D>();
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void Jump()
    {
             
    }

    private void FixedUpdate()
    {
        velocity = new Vector2(inputController.Direction.x * movementSpeed,
                               velocity.y + Physics2D.gravity.y + Time.fixedDeltaTime);
        
        playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);
        transform.position += (Vector3) playerStatus.velocity;

        Debug.Log("Hit Bottom: " + playerStatus.isCollidingBottom +"\n"+
                  "Hit Top: " + playerStatus.isCollidingTop + "\n"+
                  "Hit Left: " + playerStatus.isCollidingLeft + "\n"+
                  "Hit Right: " + playerStatus.isCollidingRight);

    }
}
