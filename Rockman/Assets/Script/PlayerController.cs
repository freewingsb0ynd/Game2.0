using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(InputController))]

public class PlayerController : MonoBehaviour {
    public float movementSpeed;
    public float jumpSpeed;

    [Space]
    public float dashSpeedMultiplier;
    public float dashTime = 0.5f;

    public bool airborneSkillAvailable { get; private set; }
    public bool isDashing { get; private set; }
    private bool isDenyingGravity;
    private InputController inputController;
    private Vector2 velocity;
    private Controller2D controller2D;
    private int faceDirection;


    public PlayerStatus playerStatus { get; private set; } 


    
    private void Awake()
    {
        inputController = GetComponent<InputController>();
        controller2D = GetComponent<Controller2D>();

        isDenyingGravity = false;

        inputController.OnMovePressed += Move;
        inputController.OnJumpPressed += JumpIfPossible;
        inputController.OnDashPressed += DashIfPossible;
        
	}

    private void OnDestroy()
    {
        inputController.OnMovePressed -= Move;
        inputController.OnJumpPressed -= JumpIfPossible;
        inputController.OnDashPressed -= DashIfPossible;
    }

    private void FixedUpdate()
    {
        WallSlideIfNeed();
        if (!isDenyingGravity) velocity.y = velocity.y + Physics2D.gravity.y + Time.fixedDeltaTime;

        playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);
        transform.position += (Vector3)playerStatus.velocity;

        if (playerStatus.isCollidingBottom || playerStatus.isCollidingTop)
        {
            velocity.y = 0;
        }

        if (playerStatus.isCollidingBottom) airborneSkillAvailable = true;

    }
    private void WallSlideIfNeed()
    {
        if ( (playerStatus.isCollidingLeft && faceDirection == -1) || (playerStatus.isCollidingRight && faceDirection == 1))
        {
            velocity.y = 0;
        }
    }
    
    public void ActivateAirborneSkill()
    {
        airborneSkillAvailable = false;
    }

    public void GravityDenied()
    {
        isDenyingGravity = true;
        StartCoroutine(CancelSpeedY());
    }
    private IEnumerator CancelSpeedY()
    {
        velocity.y = 0f;
        yield return new WaitForSeconds(dashTime);
        isDenyingGravity = false;
    }

    private void Move(float direction)
    {
        if (!isDashing)
        {
            faceDirection = (int)Mathf.Sign(direction);            
            
            velocity.x = direction * movementSpeed;
        }
    }
    
    public void Jump()
    {
        if (!isDenyingGravity)
        {
            velocity = velocity.WithY(jumpSpeed);
        }
    }

    private void JumpIfPossible()
    {
        if (playerStatus.isCollidingBottom)
        {
            Jump();
        }
    }
    
    public void Dash()
    {
        velocity.x = faceDirection * movementSpeed * dashSpeedMultiplier;
        isDashing = true;
        StartCoroutine(ActiveDash());
    }

    private void DashIfPossible()
    {
        if (!isDashing && playerStatus.isCollidingBottom)
        {
            Dash();
        }
    }
    private IEnumerator ActiveDash()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }
      
}
