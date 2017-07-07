using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour {
    public LayerMask collideMask;
    public int numberOfRays;

    private PlayerStatus playerStatus;
    private BoxCollider2D boxCollider;
    private Bounds colliderBounds;
    private RaycastOrigins raycastOrigins;
    

    private const int TOP_SIDE = 0;
    private const int BOTTOM_SIDE = 1;
    private const int LEFT_SIDE = 2;
    private const int RIGHT_SIDE = 3;
    private const float MIN_SPEED_THRESHOLD = 0.0001f;

    float stepBoundX;
    float stepBoundY;
    private float skinWidth = 0.05f;
    // Use this for initialization
    void Awake () {
        boxCollider = GetComponent<BoxCollider2D>();
        colliderBounds = boxCollider.bounds;
        colliderBounds.Expand(-2 * skinWidth);
        stepBoundX = (colliderBounds.max.x - colliderBounds.min.x) / (numberOfRays - 1);
        stepBoundY = (colliderBounds.max.y - colliderBounds.min.y) / (numberOfRays - 1);
        playerStatus = new PlayerStatus();
    }
	
	private void UpdateColliderBound () {
        colliderBounds = boxCollider.bounds;
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

    private Vector2 CheckHitHorizontal(int direction, Vector2 velocity)
    {
        float edgeToCheckHit = direction == RIGHT_SIDE ? colliderBounds.max.x : colliderBounds.min.x;
        for (int i = 0; i < numberOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(edgeToCheckHit, raycastOrigins.bottomLeft.y + i * stepBoundY),
                                                 velocity.WithY(0),
                                                 Mathf.Abs(velocity.x) + skinWidth,
                                                 collideMask);

            if (hit)
            {
                if (direction == RIGHT_SIDE) playerStatus.isCollidingRight = true;
                else playerStatus.isCollidingLeft = true;

                velocity.x = (hit.distance - skinWidth) * Mathf.Sign(velocity.x);
            }
            if (Mathf.Abs(velocity.x) > MIN_SPEED_THRESHOLD)
            {
                playerStatus.isCollidingRight = false;
                playerStatus.isCollidingLeft = false;
            }
        }


        return velocity;
    }

    private Vector2 CheckHitVertical(int direction, Vector2 velocity)
    {
        float edgeToCheckHit = direction == TOP_SIDE ? colliderBounds.max.y : colliderBounds.min.y;
        for (int i = 0; i < numberOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(raycastOrigins.bottomLeft.x + i * stepBoundX, edgeToCheckHit),
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
        if (Mathf.Abs(velocity.y) > MIN_SPEED_THRESHOLD)
        {
            playerStatus.isCollidingBottom = false;
            playerStatus.isCollidingTop = false;   
        }

        return velocity;
    }

    
    private Vector2 AdjustVelocity(Vector2 velocity)
    {
        if (velocity.x > 0) velocity = CheckHitHorizontal(RIGHT_SIDE, velocity);
        else velocity = CheckHitHorizontal(LEFT_SIDE, velocity);

        if (velocity.y > 0) velocity = CheckHitVertical(TOP_SIDE, velocity);
        else velocity = CheckHitVertical(BOTTOM_SIDE, velocity);
        

        return velocity;
	}
    
    public PlayerStatus Move(Vector2 velocity)
    {
        UpdateColliderBound();

        playerStatus.velocity = AdjustVelocity(velocity);

        return playerStatus;
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

