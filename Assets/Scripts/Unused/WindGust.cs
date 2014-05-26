using UnityEngine;
using System.Collections;

public class WindGust : MonoBehaviour {

	public float strength;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnTriggerStay2D (Collider2D c)
	{
		if (c.gameObject.tag == "Dragon")
		{
			c.rigidbody2D.AddForce(new Vector2(strength, 0));
		}
	}
}
