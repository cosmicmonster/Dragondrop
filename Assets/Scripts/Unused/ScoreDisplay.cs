using UnityEngine;
using System.Collections;

// STUPIDLY OVERCOMPLICATED WAY OF SHOWING SCORE VIA SPRITE SHEET #SPD


public class ScoreDisplay : MonoBehaviour {

	public int currentScore = 0;
	public SpriteRenderer[] numberSlots;
	public Sprite[] numbers;

	
	// Use this for initialization
	void Start () {
		if (currentScore != 0) Data.score = currentScore;
	}
	
	// Update is called once per frame
	void Update () 
	{
		DispayScore ();
	}

	void DispayScore ()
	{
		// Convert to score to text for handling
		string score = Data.score.ToString();

		// Check how many numbers the score has
		int scoreLength = score.Length;

		// This is used to make the numbers show up at the end and not front based on the total amount of slots: eg. 000XX for 00012
		int diff = 5 - scoreLength;

		// Check each number and add it to the correct position and swap to the correct sprite
		// PROBLEM: fucks up if score decreases
		for (int i = scoreLength-1; i >= 0; i--)
		{
			string tempNum = score[i].ToString (); 
			numberSlots[i+diff].sprite = numbers[int.Parse(tempNum)];
		}
	}
}
