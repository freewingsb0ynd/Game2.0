﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHeadController : MonoBehaviour {
    public PlayerController playerController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController.Die();
    }
}
