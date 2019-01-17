using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadySetGoScript : MonoBehaviour
{
	[Header("Text - Should Auto Reference")]
	public Text ReadyTextP1;
	public Text ReadyTextP2;
	public Text PedelText;

	[Header("Colours - Make sure the alpha is 1!")]
	public Color ReadyColour;
	public Color SetColour;
	public Color GoColour;

	public Rigidbody P1RB;
	public Rigidbody P2RB;

	private float StartTimer;
	private TimerScript TimeScript;
	private AudioManager SoundScript;

	internal bool GameStarted;
	private bool SoundPlayed;

    void Start()
    {
		SoundScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManager>();
		ReadyTextP1 = GetComponentsInChildren<Text>()[0];
		ReadyTextP2 = GetComponentsInChildren<Text>()[1];
		PedelText = GetComponentsInChildren<Text>()[2];
		TimeScript = GameObject.Find("TimerClock").GetComponent<TimerScript>();
		GameStarted = false;
    }


    void Update()
    {
        if (StartTimer < 7)
		{
			StartTimer += Time.deltaTime;

			switch (ConvertTimer(StartTimer))
			{
				case (0):
					P1RB.constraints = RigidbodyConstraints.FreezeAll;
					P2RB.constraints = RigidbodyConstraints.FreezeAll;
					ReadyTextP1.text = "";
					ReadyTextP2.text = "";
					PedelText.text = "";
					break;
				case (1):
					if (!SoundPlayed)
					{
						SoundScript.PlaySound("Ready", .75f);
						SoundPlayed = true;
					}
					ReadyTextP1.text = "READY";
					ReadyTextP1.color = ReadyColour;
					ReadyTextP2.text = "READY";
					ReadyTextP2.color = ReadyColour;
					PedelText.text = "START PEDELLING.....";
					break;
				case (2):
					SoundPlayed = false;
					break;
				case (3):
					ReadyTextP1.text = "SET";
					ReadyTextP1.color = SetColour;
					ReadyTextP2.text = "SET";
					ReadyTextP2.color = SetColour;
					P1RB.constraints = RigidbodyConstraints.FreezeAll;
					P2RB.constraints = RigidbodyConstraints.FreezeAll;
					if (!SoundPlayed)
					{
						SoundScript.PlaySound("Set", .75f);
						SoundPlayed = true;
					}
					break;
				case (4):
					SoundPlayed = false;
					break;
				case (5):
					ReadyTextP1.text = "GO";
					ReadyTextP1.color = GoColour;
					ReadyTextP2.text = "GO";
					ReadyTextP2.color = GoColour;
					if (!SoundPlayed)
					{
						SoundScript.PlaySound("Go", .75f);
						SoundPlayed = true;
					}
					TimeScript.StartTimer(90f);
					P1RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
					P2RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
					PedelText.text = "";
					break;
				case (6):
					SoundPlayed = false;
					ReadyTextP1.text = "";
					ReadyTextP2.text = "";
					P1RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
					P2RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
					GameStarted = true;
					break;
				default:
					break;
			}
		}
    }


	private int ConvertTimer(float Input)
	{
		return (Mathf.FloorToInt(Input));
	}
}
