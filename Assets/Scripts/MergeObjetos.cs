using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObjetos : MonoBehaviour
{
    private Vector2 mousePosition;

    private float offsetX, offsetY;

    public static bool mouseButtonReleased;

    private void OnMouseDown()
    {
        mouseButtonReleased = false;
        offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
    }

    private void OnMouseDrag()
    {
        
    }
    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }
}
