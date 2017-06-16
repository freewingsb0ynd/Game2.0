using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
    public float playerRunSpeedX;
    public float playerJumpSpeedY;
    
    public Text scoreText;

    private float score = 0;
    private static float SCREEN_HALF_WIDTH=320.0F;
    private Rigidbody2D rgBody;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rgBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rgBody.velocity = new Vector2(playerRunSpeedX,0);
        LeanTouch.OnFingerTap += Jump;
	}

    private void OnDestroy()
    {
        LeanTouch.OnFingerTap -= Jump;
    }
    // Update is called once per frame
    void Update () {
        rgBody.velocity = rgBody.velocity.WithX(playerRunSpeedX);
        if (transform.position.x > SCREEN_HALF_WIDTH)
        {
            transform.position = transform.position.WithX(transform.position.x - 2 * SCREEN_HALF_WIDTH);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //rgBody.AddForce(new Vector2(0,forceY), ForceMode2D.Impulse);
            
        }
    }

    private void Jump(LeanFinger finger)
    {
        rgBody.velocity = rgBody.velocity.WithY(playerJumpSpeedY);
        anim.SetBool("IsGrounded", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("IsGrounded", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
