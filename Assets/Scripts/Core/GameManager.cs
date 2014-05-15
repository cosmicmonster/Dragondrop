using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject 	dragon;
	public GameObject	wind;

	public Transform 	dragonSpawnPoint;
	public Transform	windSlowSpawnPoint;

	public int 			maxDragons = 3;
	public float 		dragonSpawnMinDelay = 2f,
						dragonSpawnMaxDelay = 5f;

	public float		spawnWindMinDelay = 3f,
						spawnWindMaxDelay = 5f;
	
	private float 		lastSpawnTime;
	private float 		spawnDelay;
	

	void Start () 
	{
		//ScaleSprite ();
		SpawnWind();
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
		spawnDelay = Random.Range (dragonSpawnMinDelay, dragonSpawnMaxDelay);
		Data.totalDragons++;
	}

	private void SpawnWind()
	{
		Vector2 sp = Random.Range(0,2) > 0 ? new Vector2( -10, Random.Range ( -4, 3 )) :  new Vector2( 10, Random.Range ( -4, 3 ) );

		// Create object
		Instantiate(wind, sp, Quaternion.identity);
		Invoke ("SpawnWind", Random.Range (spawnWindMinDelay, spawnWindMaxDelay));
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
