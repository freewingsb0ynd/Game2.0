using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
    public float runSpeed;
    public float jumpSpeed;
    
    public Text scoreText;

    private float score = 0;
    private static float SCREEN_HALF_WIDTH=320.0F;
    private Animator anim;
    private PlayerStatus playerStatus;
    private Controller2D controller2D;
    private Vector2 velocity;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        controller2D = GetComponent<Controller2D>();

        playerStatus.velocity = new Vector2(runSpeed, 0);
        LeanTouch.OnFingerTap += Jump;
	}

    private void OnDestroy()
    {
        LeanTouch.OnFingerTap -= Jump;
    }
    // Update is called once per frame
    void Update () {

        if (transform.position.x > SCREEN_HALF_WIDTH)
        {
            transform.position = transform.position.WithX(transform.position.x - 2 * SCREEN_HALF_WIDTH);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //rgBody.AddForce(new Vector2(0,forceY), ForceMode2D.Impulse);
            
        }

        
    }
    private void FixedUpdate()
    {
        velocity = new Vector2(runSpeed, velocity.y + Physics2D.gravity.y + Time.fixedDeltaTime);

        playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);

        transform.position += (Vector3) playerStatus.velocity;

        //transform.position += (Vector3) new Vector2(runSpeed,0);

        CheckCollisions();

    }
    private void CheckCollisions()
    {
        if (playerStatus.isCollidingBottom) anim.SetBool("IsGrounded", true);
        if (playerStatus.isCollidingTop || playerStatus.isCollidingRight) Die();
    }


    private void Jump(LeanFinger finger)
    {
        velocity = velocity.WithY(jumpSpeed);
        anim.SetBool("IsGrounded", false);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    anim.SetBool("IsGrounded", true);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("an chuoi");

        if (other.tag == "Increase Score")
        {
            score += 1;
            scoreText.text = "" + score;
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }

}
