using UnityEngine;
using System.Collections;

public class HiScore : MonoBehaviour {

	public GameObject hiScore, restart;
	public GUIText finalScore;


	private int currentHiScore;


	void Awake ()
	{
		currentHiScore = PlayerPrefs.GetInt ("HiScore", 0);
		print (currentHiScore);

		gameObject.SetActive ( false );
	}
	
	// Update is called once per frame
	void Update () {
		finalScore.text = Data.score.ToString();
	}

	public void Show ()
	{
		if (Data.score > currentHiScore)
		{
			PlayerPrefs.SetInt ("HiScore", Data.score);
			hiScore.SetActive ( true );
		} 
		else 
		{
			hiScore.SetActive ( false );
		}

		restart.SetActive ( true );
	}
}
