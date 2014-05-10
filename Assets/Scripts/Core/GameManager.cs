using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject 	dragon;
	public Transform 	dragonSpawnPoint;
	public int 			maxDragons = 3;
	public float 		minDelay = 2f,
						maxDelay = 5f;
	
	private float 		lastSpawnTime;
	private float 		spawnDelay;
	

	void Start () 
	{

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

}
