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

		if (Application.loadedLevelName == "Game") CheckMouseClickGame ();
		else CheckMouseClickMenu ();

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
	
	private void CheckMouseClickGame ()
	{
		//if ( !currentMouseOver ) return;

		if(Input.GetButton("Fire1") && currentMouseOver.tag == "Dragon")
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				currentMouseOver.GetComponent<Dragon>().Drag();
				currentMouseOver.transform.position = new Vector3(mousePos.x, currentMouseOver.transform.position.y, 1);

				//currentMouseOver.transform.position = new Vector3(mousePos.x, mousePos.y, 1);	
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			if (currentMouseOver && currentMouseOver.GetComponent<Dragon>()) currentMouseOver.GetComponent<Dragon>().Drop();
			currentMouseOver = null;
		}

	}

	private void CheckMouseClickMenu ()
	{
		if ( Input.GetButtonDown("Fire1") && currentMouseOver )
		{
			if (currentMouseOver.name == "PlayGameButton" ) Application.LoadLevel ( "Game" );
			else Debug.Log ( "Name of button not recognized!" );
		}
	}

	void OnGUI ()
	{
		//GUILayout.Label ( dragging.ToString() );
	}
}
