using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	public float 		lifeLength;
	public float		moveSpeed;
	public float		minSpeed = .3f,
						maxSpeed = 2f;

	public float		acceleration;
	public enum 		States {Idle, Moving, Dragged};

	public 				States State = States.Idle;

	private bool 		dragged = false;
	private float 		startDragTime;

	private int			dir;
	private float		currentSpeed = 0;

	// Use this for initialization
	void Start () {
		if (transform.position.x > 0) dir = -1;
		else dir = 1;

		if ( moveSpeed == 0 ) moveSpeed = Random.Range (minSpeed, maxSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckWithinBounds ();

		switch ( State )
		{
		case States.Idle:
			// Do nothing
			break;
		case States.Moving:
			Move ();
			break;
		case States.Dragged:
			break;
		}


		float timePassed = Time.time - startDragTime;

		if (dragged && timePassed > lifeLength)
		{
			GetComponent<Animator>().Play ("ScaleOut");
		}

		transform.localScale = new Vector3 (.5f, .5f);
	}

	private void Move ()
	{
		if (currentSpeed < moveSpeed) currentSpeed += acceleration * Time.deltaTime;

		float x = (transform.position.x + currentSpeed * dir * Time.deltaTime);
		
		transform.position = new Vector2(x,transform.position.y);
	}

	public void Drag ()
	{
		State = States.Dragged;
		if (!dragged) 
		{
			startDragTime = Time.time;
			dragged = true;
		}
	}

	public void Drop ()
	{
		State = States.Moving;
		currentSpeed = 0;
	}

	private void StartMove ()
	{
		State = States.Moving;
	}

	private void CheckWithinBounds ()
	{
		Camera c = Camera.main;
		if (transform.position.x > c.ScreenToWorldPoint( new Vector3 (c.pixelWidth,0f)).x + 2f ) Kill ();
		else if (transform.position.x < Camera.main.ScreenToWorldPoint( new Vector3 (0f,0f)).x - 2f ) Kill ();
	}

	public void Kill ()
	{
		Destroy ( gameObject );
	}
}
