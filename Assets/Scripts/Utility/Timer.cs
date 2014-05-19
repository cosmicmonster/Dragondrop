using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public GameObject blocker, foreground;

	private float timerDuration;
	private float degreesPerSecond;

	private float timePassed;
	private bool running = false;
	private bool isDone = false;

	// Update is called once per frame
	void Update () 
	{
		if ( !running || isDone ) return;
		
		timePassed = Time.timeSinceLevelLoad-Data.countdownTime % timerDuration;

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

		if (timerDuration - timePassed <= Time.deltaTime) isDone = true;

	}

	public void StartTimer (float time)
	{
		timerDuration = time;
		degreesPerSecond = 360 / timerDuration; // Based on how long it takes to make a full scirlce in 1 minute, degrees per second

		running = true;
	}

	public bool IsRunning (){ return running; }
	public bool IsDone (){ return isDone; }
}
