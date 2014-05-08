using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject dragon;
	public int maxDragons = 3;
	public float minDelay = 2f,
	maxDelay = 5f;

	private List<GameObject> dragons = new List<GameObject>();
	private float lastSpawnTime;
	private float spawnDelay;
	

	void Start () 
	{

	}

	void Update () 
	{
		if (dragons.Count < maxDragons && Time.time - lastSpawnTime > spawnDelay)
		{
			SpawnDragon ();
		}
	}


	private void SpawnDragon ()
	{
		GameObject tDragon = Instantiate(dragon, new Vector3 (0,0,1), Quaternion.identity) as GameObject;
		dragons.Add(tDragon);
		lastSpawnTime = Time.time;
		spawnDelay = Random.Range (minDelay, maxDelay);
	}

}
