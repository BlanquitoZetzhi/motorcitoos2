using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
	private bool isDragging = false;
	private bool hasBeenDropped = false;
	private bool isOverValidZone = false;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (hasBeenDropped) return; 

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
			touchWorldPos.z = 0;

			if (touch.phase == TouchPhase.Began)
			{
				Collider2D hit = Physics2D.OverlapPoint(touchWorldPos);
				if (hit != null && hit.gameObject == this.gameObject)
				{
					isDragging = true;
					rb.velocity = Vector2.zero;
					rb.gravityScale = 0;
					offset = transform.position - touchWorldPos;
				}
			}

			if (touch.phase == TouchPhase.Moved && isDragging)
			{
				transform.position = touchWorldPos + offset;
			}

			if (touch.phase == TouchPhase.Ended && isDragging)
			{
				isDragging = false;

				if (isOverValidZone)
				{
					hasBeenDropped = true;
					rb.gravityScale = 1;
				}
				else
				{
					rb.gravityScale = 1;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("ZonaFusion"))
		{
			isOverValidZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("ZonaFusion"))
		{
			isOverValidZone = false;
		}
	}
}
