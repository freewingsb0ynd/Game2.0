using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviourScript : MonoBehaviour {
    private Vector2 savedPos;
    private Vector3 dist;

    public void BeginDrag(){
        dist = Camera.main.WorldToScreenPoint(transform.position);
        savedPos.x = Input.mousePosition.x - dist.x;
        savedPos.y = Input.mousePosition.y - dist.y;
    }

    public void Drag(){
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - savedPos.x, 
                                                                        Input.mousePosition.y - savedPos.y,
                                                                        dist.z));
    }
}
