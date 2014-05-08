using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public float startFlySkill;
	public float learnSpeed;
	public enum States { Idle, Entering, Flying, Learning };
	public States State;

	private float flySkill = 0f;
	private float maxFlySkill = 100f;
	private Transform skillBar;

	private float amplitude;

	// Use this for initialization
	void Start () 
	{
		State = States.Entering;
		skillBar = transform.GetChild(0);
		flySkill = startFlySkill;
		amplitude = Random.Range ( 0.05f, 0.02f );
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch ( State )
		{
		case States.Idle:
			Idle ();
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
		
		if (flySkill < maxFlySkill) flySkill += learnSpeed * Time.deltaTime;
		else  
		{ 
			flySkill = maxFlySkill; 
			Debug.Log ("Dragon has learnt to fly!!");
			Score.score += Score.pointsPerDragon;
			State = States.Flying;
		}
		
		UpdateSkillBar();
	}


	// After spawning the dragon moves into the screen
	private void Entering ()
	{

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

	// When being dragged, don't do anything
	private void Idle ()
	{
		rigidbody2D.gravityScale = 0;
	}
	
	private void UpdateSkillBar ()
	{
		if (!skillBar.gameObject.activeSelf) 
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
		if (c.gameObject.tag == "Level") State = States.Idle;
	}

	void OnCollisionExit2D (Collision2D c)
	{
		State = States.Learning;
	}

	public void PickUp() { State = States.Idle; }
	public void LetGo() {State = States.Learning; }
	public States GetState () { return State; }
	//private void OnDestroy () { Debug.Log ("Dragonling " + gameObject.GetInstanceID() + " has died :'("); }

}
