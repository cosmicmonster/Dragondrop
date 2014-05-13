﻿using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {
	
	// Attach this script to an orthographic camera.
	
	public Transform currentObject; 
	private float offSetX, offSetY;
	
	
	
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) 
		{  
			if (!currentObject) 
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				
				if (hit && hit.transform.gameObject.GetComponent<ClickableObject>())
				{
					currentObject = hit.transform;
					offSetX = currentObject.position.x - hit.point.x;
					offSetY = currentObject.position.y - hit.point.y; 
					//Debug.Log ( Vector3.Distance( currentObject.position, hit.point.x ));
				}	
			}
		}
		else if (Input.GetButtonUp("Fire1")) 
		{
			if (!currentObject) return;

			if (currentObject.gameObject.tag == "Dragon")
			{
				currentObject.gameObject.GetComponent<Dragon>().Drop();
			}

			currentObject = null;
		}
		
		if (currentObject) 
		{
			if (currentObject.gameObject.tag == "Dragon")
			{
				currentObject.gameObject.GetComponent<Dragon>().Drag();
				currentObject.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, currentObject.position.y, currentObject.position.z);
			} 
			else 
			{
				currentObject.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offSetX, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offSetY, currentObject.position.z);
			}

		}
	}
}