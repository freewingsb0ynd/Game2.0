using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceBehaviourScript : EventTrigger {
    private Vector2 mouseOffset;
    
    private void Start()
    {
        transform.localPosition = new Vector3(
            Random.Range(-277.5f, 277.5f),
            Random.Range(-194f, 224f),
            0);

        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 canvasMouse = transform.parent.InverseTransformPoint(
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition));

        mouseOffset = canvasMouse - transform.localPosition;
    }

    public override void OnDrag(PointerEventData data)
    {
        Vector3 canvasMouse = transform.parent.InverseTransformPoint(
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 imgPos = (Vector2)canvasMouse - mouseOffset;

        transform.localPosition = new Vector3(
            Mathf.Clamp(imgPos.x, -277.5f, 277.5f),
            Mathf.Clamp(imgPos.y, -294f, 294f),
            0);
    }
}
