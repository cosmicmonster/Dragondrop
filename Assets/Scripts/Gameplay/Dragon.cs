using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {
	
	public float startFlySkill;
	public float learnSpeed;
	public float walkAmount;
	public float enteringSpeed;

	public enum States { Waiting, Dragging, Entering, Flying, Learning };
	public States State;

	private float flySkill = 0f;
	private float maxFlySkill = 100f;
	private Transform skillBar;

	private float amplitude;
	private Vector3 startPos;

	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		startPos = transform.position;
		State = States.Entering;
		walkAmount = walkAmount + Random.Range ( 1f, 1.5f );
		skillBar = transform.GetChild(0);
		flySkill = startFlySkill;
		amplitude = Random.Range ( 0.05f, 0.02f );
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch ( State )
		{
		case States.Waiting:
			Waiting ();
			break;
		case States.Dragging:
			Dragging ();
			break;
		case States.Entering:
			Entering ();
			break;
		case States.Learning:
			Learning ();
			break;
		case States.Flying:
			Flying ();
			break;
		}
	}


	// When the dragon is not being dragged or is not on solid ground and is learning to fly
	private void Learning ()
	{
		rigidbody2D.gravityScale = 1f;

		CancelInvoke ();
		
		if (flySkill < maxFlySkill) flySkill += learnSpeed * Time.deltaTime;
		else  
		{ 
			flySkill = maxFlySkill; 
			Debug.Log ("Dragon has learnt to fly!!");
			Data.score += Data.pointsPerDragon;

			// Hide Skill bar elements
			HideChildren ();
			State = States.Flying;
			anim.Play ("dragon_flying");

		}
		
		UpdateSkillBar();
	}


	// After spawning the dragon moves into the screen
	private void Entering ()
	{
		transform.position = Vector3.Lerp ( transform.position, new Vector3 ( startPos.x + walkAmount, transform.position.y ), Time.deltaTime );

		anim.Play ("dragon_idle");

		Invoke ("StartWaiting", 2f); 
	}


	// After successfully learning how to fly the dragon flies away
	private void Flying ()
	{
		// FLY LITTLE DRAGON FLY!
		rigidbody2D.gravityScale = 0f;
		float x = transform.position.x + Time.deltaTime;
		float y = transform.position.y + Mathf.PingPong(Time.time, .2f) * amplitude;
		
		transform.position = new Vector2(x,y);
	}

	private void StartWaiting ()
	{
		State = States.Waiting;

		anim.Play ("dragon_idle");
	}

	// When being dragged, don't do anything
	private void Waiting ()
	{
		rigidbody2D.gravityScale = 0;
	}

	private void Dragging ()
	{
		rigidbody2D.gravityScale = 0;
	}
	
	private void UpdateSkillBar ()
	{
		if (!skillBar.gameObject.activeSelf && State != States.Flying) 
		{
			skillBar.gameObject.SetActive ( true );
			transform.GetChild(1).gameObject.SetActive ( true ); 
		}
		else 
		{
			skillBar.localScale = new Vector3(flySkill/maxFlySkill, skillBar.localScale.y, skillBar.localScale.z);
		}
	}

	void OnCollisionStay2D (Collision2D c)
	{
		if (c.gameObject.tag == "Level" && State != States.Entering && State != States.Dragging) 
		{
			State = States.Waiting;
			anim.Play ("dragon_idle");
		}
	}

	void OnCollisionExit2D (Collision2D c)
	{
		if (c.gameObject.tag != "Dragon" && State != States.Dragging ) 
		{
			State = States.Learning;
		}
	}

	private void HideChildren ()
	{
		foreach ( Transform t in transform )
		{
			t.gameObject.SetActive ( false );
		}
	}
	
	public void Drag() { State = States.Dragging; anim.Play ("dragon_drag"); CancelInvoke ();}
	public void Drop() {State = States.Learning; anim.Play ("dragon_learning");}
	public States GetState () { return State; }
	private void OnDestroy () { Data.totalDragons--; }

}
