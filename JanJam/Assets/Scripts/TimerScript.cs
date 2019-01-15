using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

	public float Timer;

	private string Secs;
	private string Mins;

	private bool TimerStarted;
	private GameController Controller;


	private void Start()
	{
		Controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	private void Update()
	{
		if (TimerStarted)
		{
			if (Timer > 0)
			{
				Timer -= Time.deltaTime;
			}
			else
			{
				Controller.ChangeScene(Scenes.End);
			}
		}
	}

	public string DisplayTimer()
	{
		Mins = Mathf.Floor(Timer / 60).ToString("0");
		Secs = Mathf.Floor(Timer % 60).ToString("00");

		return (Mins + ":" + Secs);
	}

	internal void StartTimer()
	{
		TimerStarted = true;
	}

	internal void StartTimer(float Value)
	{
		Timer = Value;
		TimerStarted = true;
	}

	internal void StopTimer()
	{
		TimerStarted = false;
	}
}
