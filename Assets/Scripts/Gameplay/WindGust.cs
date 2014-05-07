using UnityEngine;
using System.Collections;

public class WindGust : MonoBehaviour {

	public float strength = 1f;
	public float gustInterval = 3f;
	public float lifeLength = 30f;

	private float spawnTime;
	private bool started = false;



	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		InvokeRepeating ("Gust", 1f, gustInterval);
	}
	
	// Update is called once per frame
	void Update () {
		CheckAlive ();
	}

	private void Gust ()
	{
		//Debug.Log ("Pffffffffffffffthhhhhhzzz");
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.gameObject.tag == "Dragon")
		{
			if ( !started ) started = true;
			c.rigidbody2D.AddForce(new Vector2(0, 1000f));
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
