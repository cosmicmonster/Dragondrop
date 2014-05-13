using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject 	dragon;
	public GameObject	windSlow;

	public Transform 	dragonSpawnPoint;
	public Transform	windSlowSpawnPoint;

	public int 			maxDragons = 3;
	public float 		minDelay = 2f,
						maxDelay = 5f;
	
	private float 		lastSpawnTime;
	private float 		spawnDelay;
	

	void Start () 
	{
		//ScaleSprite ();
	}

	void Update () 
	{
		if (Data.totalDragons < maxDragons && Time.time - lastSpawnTime > spawnDelay)
		{
			SpawnDragon ();
		}
	}


	private void SpawnDragon ()
	{
		Instantiate(dragon, dragonSpawnPoint.position, Quaternion.identity);
		lastSpawnTime = Time.time;
		spawnDelay = Random.Range (minDelay, maxDelay);
		Data.totalDragons++;
	}

	private void SpawnWindSlow ()
	{


		// Create object
		Instantiate(windSlow, windSlowSpawnPoint.position, Quaternion.identity);
		// Spawn another wind after 5 seconds. This is most likely going
		// to depend on at least a couple of factors
		Invoke ("SpawnWindSlow", 5);
	}

	private void ScaleSprite ()
	{
		Transform screenTest = null;
		if (!screenTest) return;

		SpriteRenderer sr = screenTest.GetComponent<SpriteRenderer>();
		if (sr == null) return;
				
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		screenTest.transform.localScale = new Vector3( worldScreenWidth / width, worldScreenHeight / height );
	}

}
