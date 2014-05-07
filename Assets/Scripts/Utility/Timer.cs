using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public GameObject blocker, foreground;

	public float timerDuration = 10f;
	private float degreesPerSecond;

	private float timePassed;

	// Use this for initialization
	void Start () {
		degreesPerSecond = 360 / timerDuration; // Based on how long it takes to make a full scirlce in 1 minute, degrees per second
	}
	
	// Update is called once per frame
	void Update () {

		timePassed = Time.time % timerDuration;

		if (timePassed < timerDuration/2)
		{
			// If blocker is deactivated, show it
			if ( !blocker.activeSelf )  blocker.SetActive ( true );

			// Reset foreground position
			foreground.transform.rotation = Quaternion.AngleAxis( 180, Vector3.forward );
			blocker.transform.rotation = Quaternion.AngleAxis (180 + (-timePassed * degreesPerSecond), Vector3.forward);
		} 
		else
		{
			// Hide blocker so that foreground can been seen
			blocker.SetActive ( false );
			foreground.transform.rotation = Quaternion.AngleAxis (-timePassed * degreesPerSecond, Vector3.forward);
		}

	}


	void OnGUI ()
	{
		GUILayout.Label (timePassed.ToString());
	}
}
