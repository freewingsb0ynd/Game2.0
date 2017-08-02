using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public float moveSpeed;
    public LayerMask collisionMask;

    [SyncVar(hook = "OnServerPositionChanged")]
    private Vector2 serverPosition;

    private Coroutine observerCoroutine;
    private Vector2 velocity = Vector2.zero;

    [Command]
    private void CmdMove(Vector2 direction)
    {
        Vector2 displacement = direction.normalized * moveSpeed * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            moveSpeed * Time.fixedDeltaTime + 0.01f,
            collisionMask
        );

        if (hit)
        {
            displacement = displacement.ScaleTo(hit.distance - 0.01f);
        }

        transform.position += (Vector3)displacement;
        serverPosition = transform.position;
    }

    private void OnServerPositionChanged(Vector2 position)
    {
        //if (isLocalPlayer) return;

        if(observerCoroutine != null)
        {
            StopCoroutine(observerCoroutine);
        }
        observerCoroutine = StartCoroutine(MoveTo(position));
    }

    IEnumerator MoveTo(Vector2 newPos)
    {
        Vector2 startPosition = transform.position;
        float time = 0;
        while(time < 0.1f)
        {
            transform.position = Vector2.Lerp(startPosition, newPos, time / 0.1f);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = newPos;
        observerCoroutine = null;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            InputController.Instance.OnMove += OnMove;
        }
    }

    private void OnDestroy()
    {
        if (isLocalPlayer)
        {
            InputController.Instance.OnMove -= OnMove;
        }
    }

    private void Update()
    {
        if (isServer)
        {
            serverPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        CmdMove(velocity);
    }

    private void OnMove(Vector2 direction)
    {
        velocity = direction.normalized;
    }
}
