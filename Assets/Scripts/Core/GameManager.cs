using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject 	dragon;
	public GameObject	wind;
	public GameObject	ui;
	public GUIText		score;
	public GUIText		countDownText;
	public Timer		timer;

	public Transform 	dragonSpawnPoint;
	public Transform	windSlowSpawnPoint;

	public float		gameDuration = 60f;

	public int 			maxDragons = 3;
	public float 		dragonSpawnMinDelay = 2f,
						dragonSpawnMaxDelay = 5f;

	public float		spawnWindMinDelay = 3f,
						spawnWindMaxDelay = 5f;
	
	private float 		lastSpawnTime;
	private float 		spawnDelay;

	private bool 		gameOver = false;
	private bool		countdown = true;
	

	void Awake () 
	{
		//ScaleSprite ();
		Application.targetFrameRate = 60;
	}

	void Update () 
	{	
		if ( gameOver ) return;
		else if ( countdown ) 
		{
			Countdown ();
			return;
		}

		if (!timer.IsRunning () && !timer.IsDone ())
		{

		}

		if (Data.totalDragons < maxDragons && Time.time - lastSpawnTime > spawnDelay)
		{
			SpawnDragon ();
		}


		// Check if Time is up
		if ( timer.IsDone () )
		{
			Camera.main.gameObject.GetComponent<Animator>().Play ("camera_gameover");
			Invoke ("AnimateScore", .5f);
			gameOver = true;
		}

		// Show current score on GUI Text Element
		score.text = Data.score.ToString();
	}

	private void Countdown ()
	{
		int cd = Mathf.CeilToInt(Data.countdownTime - Time.timeSinceLevelLoad);

		countDownText.text = cd.ToString();

		if (cd <= 0) {
			StartGame ();
		}
	}

	private void StartGame ()
	{
		countdown = false;
		countDownText.gameObject.SetActive ( false );
		timer.gameObject.SetActive ( true );
		timer.StartTimer ( gameDuration );
		SpawnWind();
	}
	
	private void AnimateScore ()
	{
		ui.SetActive ( true );
		//ui.GetComponent<HiScore>().Show ();
		ui.GetComponent<Animator>().Play ("hi_score");
	}
	
	private void SpawnDragon ()
	{
		Instantiate(dragon, dragonSpawnPoint.position, Quaternion.identity);
		lastSpawnTime = Time.time;
		spawnDelay = Random.Range (dragonSpawnMinDelay, dragonSpawnMaxDelay);
		Data.totalDragons++;
	}

	private void SpawnWind()
	{
		if ( gameOver ) return;
		Vector2 sp = Random.Range(0,2) > 0 ? new Vector2( -10, Random.Range ( -3, .8f )) :  new Vector2( 10, Random.Range ( -3, .8f ) );

		// Create object
		Instantiate(wind, sp, Quaternion.identity);
		Invoke ("SpawnWind", Random.Range (spawnWindMinDelay, spawnWindMaxDelay));
	}

	void OnGUI ()
	{
		if ( Application.isEditor )
		{
			GUILayout.Label ("DEBUGTONS");
			if (GUILayout.Button ("Reset Hi-Score"))
			{
				PlayerPrefs.SetInt ("HiScore", 0);
				Debug.Log ("Reset Hi-Score");
			}
			if (GUILayout.Button ("+100"))
			{
				Data.score += 100;
				Debug.Log (Data.score);
			}
		}
	}
}
