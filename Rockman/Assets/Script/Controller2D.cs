﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour {
    public LayerMask collideMask;
    public int numRaycastHorizontal = 2;
    public int numRaycastVertical = 2;
    
    private BoxCollider2D boxCollider2D;
    private Bounds colliderBounds;
    private RaycastOrigins raycastOrigins;
    

    private const int TOP_SIDE = 0;
    private const int BOTTOM_SIDE = 1;
    private const int LEFT_SIDE = 2;
    private const int RIGHT_SIDE = 3;
    private const float MIN_SPEED_THRESHOLD = 0.0001f;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private float skinWidth = 0.05f;
    // Use this for initialization
    void Awake () {
        boxCollider2D = GetComponent<BoxCollider2D>();
        numRaycastHorizontal = numRaycastHorizontal < 2 ? 2 : numRaycastHorizontal;
        numRaycastVertical = numRaycastVertical < 2 ? 2 : numRaycastVertical;
        InitRaycastSpacing();
    }
    private void InitRaycastSpacing()
    {
        colliderBounds = boxCollider2D.bounds;
        colliderBounds.Expand(-2 * skinWidth);
        horizontalRaySpacing = (colliderBounds.max.x - colliderBounds.min.x) / (numRaycastHorizontal - 1);
        verticalRaySpacing = (colliderBounds.max.y - colliderBounds.min.y) / (numRaycastVertical - 1);
    }
	
	private void UpdateColliderBound () {
        colliderBounds = boxCollider2D.bounds;
        colliderBounds.Expand(-2 * skinWidth);
        UpdateRaycastOrigins();
	}

    private void UpdateRaycastOrigins()
    {
        raycastOrigins.topLeft = new Vector2(colliderBounds.min.x, colliderBounds.max.y);
        raycastOrigins.topRight = new Vector2(colliderBounds.max.x, colliderBounds.max.y);
        raycastOrigins.bottomLeft = new Vector2(colliderBounds.min.x, colliderBounds.min.y);
        raycastOrigins.bottomRight = new Vector2(colliderBounds.max.x, colliderBounds.min.y);
    }

    private PlayerStatus CheckHitHorizontal(PlayerStatus playerStatus)
    {
        Vector2 velocity = playerStatus.velocity;

        int direction = velocity.x > 0 ? RIGHT_SIDE : LEFT_SIDE;

        float edgeToCheckHit = direction == RIGHT_SIDE ? colliderBounds.max.x : colliderBounds.min.x;
        for (int i = 0; i < numRaycastVertical; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(edgeToCheckHit, raycastOrigins.bottomLeft.y + i * verticalRaySpacing),
                                                 velocity.WithY(0),
                                                 Mathf.Abs(velocity.x) + skinWidth,
                                                 collideMask);

            if (hit)
            {
                if (direction == RIGHT_SIDE) playerStatus.isCollidingRight = true;
                else playerStatus.isCollidingLeft = true;

                velocity.x = (hit.distance - skinWidth) * Mathf.Sign(velocity.x);
            }
            
        }
        playerStatus.velocity = velocity;

        return playerStatus;
    }

    private PlayerStatus CheckHitVertical(PlayerStatus playerStatus)
    {
        Vector2 velocity = playerStatus.velocity;

        int direction = velocity.y > 0 ? TOP_SIDE : BOTTOM_SIDE;

        float edgeToCheckHit = direction == TOP_SIDE ? colliderBounds.max.y : colliderBounds.min.y;
        for (int i = 0; i < numRaycastHorizontal; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(raycastOrigins.bottomLeft.x + i * horizontalRaySpacing, edgeToCheckHit),
                                                 velocity.WithX(0),
                                                 Mathf.Abs(velocity.y) + skinWidth,
                                                 collideMask);

            if (hit)
            {
                if (direction == TOP_SIDE) playerStatus.isCollidingTop = true;
                else playerStatus.isCollidingBottom = true;

                velocity.y = (hit.distance - skinWidth) * Mathf.Sign(velocity.y);
            }
            
        }
        playerStatus.velocity = velocity;

        return playerStatus;
    }

    public PlayerStatus Move(Vector2 velocity)
    {
        UpdateColliderBound();

        PlayerStatus result = new PlayerStatus {
            velocity = velocity,
            isCollidingBottom = false,
            isCollidingLeft = false,
            isCollidingRight = false,
            isCollidingTop = false
        };

        result = CheckHitHorizontal(result);
        result = CheckHitVertical(result);
        return result;
    }
}

struct RaycastOrigins
{
    public Vector2 topLeft;
    public Vector2 topRight;
    public Vector2 bottomLeft;
    public Vector2 bottomRight;
}

public struct PlayerStatus
{
    public Vector2 velocity;
    public bool isCollidingTop;
    public bool isCollidingRight;
    public bool isCollidingBottom;
    public bool isCollidingLeft;
}

