using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	public float strength = 1f;
	public float gustInterval = 3f;
	public float lifeLength = 30f;

	private float spawnTime;



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
			Debug.Log ("SNNESNSNS");
			c.rigidbody2D.AddForce(new Vector2(0, 1000f));
		}
	}

	void CheckAlive ()
	{
		if (Time.time - spawnTime > lifeLength )
		{
			Destroy (gameObject);
		}
	}

	void OnGUI ()
	{
		GUILayout.Label ((Time.time - spawnTime).ToString ());
	}
}
