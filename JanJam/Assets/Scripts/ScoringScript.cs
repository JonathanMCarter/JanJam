using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoringScript : MonoBehaviour
{

	public float Player1Score;

	public float Player2Score;


	internal void Player1Scored(float Multi)
	{
		Player1Score += 1 * Multi;
	}

	internal void Player2Scored(float Multi)
	{
		Player2Score += 1 * Multi;
	}

	internal void ResetScript()
	{
		Player1Score = 0;
		Player2Score = 0;
	}
}
