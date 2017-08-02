using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour {
    public static InputController Instance { get; private set; }

    public Action<Vector2> OnMove;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(OnMove != null)
        {
            OnMove(new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ));
        }
    }
}
