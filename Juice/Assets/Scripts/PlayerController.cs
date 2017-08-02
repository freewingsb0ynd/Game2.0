using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
    public float moveSpeed;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Hammer");
        }
        
        transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * moveSpeed * Time.fixedDeltaTime;
        
        anim.SetFloat("MoveVertical", Input.GetAxisRaw("Vertical"));
    }
}
