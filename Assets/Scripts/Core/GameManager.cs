using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject 	dragon;
	public GameObject	wind;
	public GameObject	ui;
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
	

	void Start () 
	{
		//ScaleSprite ();
		timer.StartTimer ( gameDuration );
		SpawnWind();
	}

	void Update () 
	{	
		if ( gameOver ) return;

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
		Vector2 sp = Random.Range(0,2) > 0 ? new Vector2( -10, Random.Range ( -4, 3 )) :  new Vector2( 10, Random.Range ( -4, 3 ) );

		// Create object
		Instantiate(wind, sp, Quaternion.identity);
		Invoke ("SpawnWind", Random.Range (spawnWindMinDelay, spawnWindMaxDelay));
	}
}
