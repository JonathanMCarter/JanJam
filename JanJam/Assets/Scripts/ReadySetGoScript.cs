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


    void Start()
    {
		ReadyTextP1 = GetComponentsInChildren<Text>()[0];
		ReadyTextP2 = GetComponentsInChildren<Text>()[1];
		PedelText = GetComponentsInChildren<Text>()[2];
		TimeScript = GameObject.Find("TimerClock").GetComponent<TimerScript>();
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
					P2RB.constraints = RigidbodyConstraints.FreezePosition;
					ReadyTextP1.text = "";
					ReadyTextP2.text = "";
					PedelText.text = "";
					break;
				case (1):
					ReadyTextP1.text = "READY";
					ReadyTextP1.color = ReadyColour;
					ReadyTextP2.text = "READY";
					ReadyTextP2.color = ReadyColour;
					PedelText.text = "START PEDELLING.....";
					break;
				case (3):
					ReadyTextP1.text = "SET";
					ReadyTextP1.color = SetColour;
					ReadyTextP2.text = "SET";
					ReadyTextP2.color = SetColour;
					break;
				case (5):
					ReadyTextP1.text = "GO";
					ReadyTextP1.color = GoColour;
					ReadyTextP2.text = "GO";
					ReadyTextP2.color = GoColour;
					TimeScript.StartTimer(90f);
					P1RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
					P2RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
					PedelText.text = "";
					break;
				case (6):
					ReadyTextP1.text = "";
					ReadyTextP2.text = "";
					P1RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
					P2RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
