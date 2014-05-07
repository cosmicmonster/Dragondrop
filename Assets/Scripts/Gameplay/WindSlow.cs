using UnityEngine;
using System.Collections;

public class WindSlow : MonoBehaviour {

	public float dampening = 5f;
	public float lifeLength = 30f;

	private float spawnTime;
	private bool started = false;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		CheckAlive ();
	}
	

	void OnTriggerStay2D (Collider2D c)
	{


		if (c.gameObject.tag == "Dragon")
		{
			Debug.Log ("pffffffffff");
			if ( !started ) { started = true; spawnTime = Time.time; }
			c.rigidbody2D.AddForce(new Vector2(0, dampening));
		}
	}

	void CheckAlive ()
	{
		if (started && Time.time - spawnTime > lifeLength )
		{
			Destroy (gameObject);
		}
	}

	void OnGUI ()
	{
		GUILayout.Label ((Time.time - spawnTime).ToString ());
	}
}
