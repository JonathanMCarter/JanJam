using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

	public float Timer;

	private string Secs;
	private string Mins;


	private void Update()
	{
		if (Timer > 0)
		{
			Timer -= Time.deltaTime;
		}
		else
		{
			// end game
		}
	}



	public string DisplayTimer()
	{
		Mins = Mathf.Floor(Timer / 60).ToString("00");
		Secs = Mathf.Floor(Timer % 60).ToString("00");

		return (Mins + ":" + Secs);
	}
}
