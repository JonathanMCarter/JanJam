using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour
{

	public enum Element
	{
		Player1,
		Player2,
		Timer,
	};

	public Element WhichElement;
	private Text text;
	private ScoringScript ScoreScript;
	private TimerScript TimeScript;


	private void Start()
	{
		text = GetComponent<Text>();
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();

		if (GetComponent<TimerScript>())
		{
			TimeScript = GetComponent<TimerScript>();
		}
	}

	private void Update()
	{
		switch (WhichElement)
		{
			case Element.Player1:
				text.text = "SCORE: " + ScoreScript.Player1Score.ToString();
				break;
			case Element.Player2:
				text.text = "SCORE: " + ScoreScript.Player2Score.ToString();
				break;
			case Element.Timer:
				text.text = TimeScript.DisplayTimer();
				break;
			default:
				break;
		}
	}

}
