using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoringScript : MonoBehaviour
{

	public float Player1Score;
	public float Player1Multi;

	public float Player2Score;
	public float Player2Multi;


	internal void Player1Scored()
	{
		Player1Score += 1 * Player1Multi;
	}

	internal void Player2Scored()
	{
		Player2Score += 1 * Player2Multi;
	}

	internal void ResetScript()
	{
		Player1Score = 0;
		Player2Score = 0;
		Player1Multi = 1;
		Player2Multi = 1;
	}
}
