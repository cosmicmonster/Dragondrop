using UnityEngine;
using System.Collections;

public class WindSlow : MonoBehaviour {

	public float dampening = 5f;

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.gameObject.tag == "Dragon")
		{
		 	if (c.gameObject.GetComponent<Dragon>().GetState() == Dragon.States.Dragging) return;
			else c.rigidbody2D.AddForce(new Vector2(0, dampening));
		}
	}
}
