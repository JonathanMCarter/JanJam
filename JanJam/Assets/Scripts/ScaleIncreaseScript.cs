using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleIncreaseScript : MonoBehaviour
{

	public float Scale;

	private ScoringScript ScoreScript;


	private void Start()
	{
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();
	}


	private void Update()
	{
		Scale = ScoreScript.Player1Score * 2;
		transform.localScale = Vector3.one * Scale;
	}
}
