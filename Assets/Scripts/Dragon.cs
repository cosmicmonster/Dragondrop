using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public float startFlySkill;
	public float learnSpeed;
	public enum State { Entering, Flying, Dragged, Learning };
	public State States;

	private float flySkill = 0f;
	private float maxFlySkill = 100f;
	private Transform skillBar;
	private bool canFly = false;
	private bool pauseTraining = true;
	private bool grounded = false;
	private bool pickedUp = false;

	private float amplitude;

	// Use this for initialization
	void Start () {
		skillBar = transform.GetChild(0);
		flySkill = startFlySkill;
		amplitude = Random.Range ( 0.05f, 0.02f );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( !canFly)
		{
			if ( grounded || pickedUp ) pauseTraining = true;
			else pauseTraining = false;

			if (!pauseTraining)
			{
				if (flySkill < maxFlySkill) flySkill += learnSpeed * Time.deltaTime;
				else  
				{ 
					flySkill = maxFlySkill; 
					canFly = true;
					Debug.Log ("Dragon has learnt to fly!!");
				}

				UpdateSkillBar();
			}
		}
		else 
		{
			// FLY LITTLE DRAGON FLY!
			rigidbody2D.gravityScale = 0f;
			float x = transform.position.x + Time.deltaTime;
			float y = transform.position.y + Mathf.PingPong(Time.time, 1f) * amplitude;
			//float y = Mathf.Sin (x) * 1f;

			transform.position = new Vector2(x,y);
		}
	}

	private void UpdateSkillBar ()
	{
		skillBar.localScale = new Vector3(flySkill/maxFlySkill, skillBar.localScale.y, skillBar.localScale.z);
	}

	void OnCollisionStay2D (Collision2D c)
	{
		if (c.gameObject.tag == "Level") grounded = true;
	}

	void OnCollisionExit2D (Collision2D c)
	{
		grounded = false;
	}

	public void PickUp() { pickedUp = true; }
	public void LetGo() { pickedUp = false; }


}
