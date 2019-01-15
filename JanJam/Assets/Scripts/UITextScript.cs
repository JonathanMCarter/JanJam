using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour
{

	public enum Player
	{
		Player1,
		Player2,
	};

	public Player WhichPlayer;
	private Text text;
	private ScoringScript ScoreScript;


	private void Start()
	{
		text = GetComponent<Text>();
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();
	}

	private void Update()
	{
		switch (WhichPlayer)
		{
			case Player.Player1:
				text.text = "SCORE: " + ScoreScript.Player1Score.ToString();
				break;
			case Player.Player2:
				text.text = "SCORE: " + ScoreScript.Player2Score.ToString();
				break;
			default:
				break;
		}
	}

}
