using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveRight(300));
	}
	
	private IEnumerator MoveRight(float distance)
    {
        float timeToAnimate = distance / 100;
        float time = 0;
        Vector2 startPos = transform.localPosition;
        Vector2 targetPos = transform.localPosition + (Vector3.right * distance);

        while (time < timeToAnimate)
        {
            transform.localPosition = Vector2.Lerp(
                startPos,
                targetPos,
                time / timeToAnimate
                );
            time += Time.deltaTime;
            yield return null;
        }
        
    }
}
