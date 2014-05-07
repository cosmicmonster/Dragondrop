using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {


	private GameObject currentMouseOver;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckMouseOver ();
		CheckMouseClick ();
	}


	private void CheckMouseOver ()
	{
		// Casting a ray in order to check if mouse is over clickable object
		
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if(!hit) { return; }
		else {
			currentMouseOver = hit.transform.gameObject.GetComponent<ClickableObject>() ? hit.transform.gameObject : null;
		}
	}
	
	private void CheckMouseClick ()
	{
		if(Input.GetButton("Fire1"))
		{
			if(currentMouseOver)
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				currentMouseOver.transform.position = new Vector3(mousePos.x, mousePos.y, 1);
				currentMouseOver.GetComponent<Dragon>().PickUp();
			}
		} 
		else if (Input.GetButtonUp("Fire1"))
		{
			if (currentMouseOver) currentMouseOver.GetComponent<Dragon>().LetGo();
			currentMouseOver = null;
		}
	}
}
