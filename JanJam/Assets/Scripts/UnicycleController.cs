using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
	Bike1,
	Bike2,
};

public class UnicycleController : MonoBehaviour
{
	public Player WhichUnicycle;

	public GameObject Pedals;
	public GameObject Wheel;
	public GameObject SpikedWheel;
	public GameObject RocketPower;

	private Rigidbody RB;
	private Transform TS;

	[Header("Player1")]
	public float MoveSpeedP1;
	public float MinSpeedP1 = 3f;
	public float MaxSpeedP1 = 10f;
	public float AccelerationSpeedP1 = 4f;

	public bool PressedLeftP1;
	public Tiers P1Tier;

	[Header("Player2")]
	public float MoveSpeedP2;
	public float MinSpeedP2 = 3f;
	public float MaxSpeedP2 = 10f;
	public float AccelerationSpeedP2 = 4f;

	public bool PressedLeftP2;
	public Tiers P2Tier;

	private bool HasSpikedWheels;
	private bool HasRockets;

	private ScoringScript ScoreScript;
	private ReadySetGoScript ReadyScript;
	private AudioManager SoundScript;

	void Start()
	{
		RB = GetComponent<Rigidbody>();
		TS = GetComponent<Transform>();
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();
		ReadyScript = GameObject.Find("ReadySetGoController").GetComponent<ReadySetGoScript>();
		SoundScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManager>();
    }


    void Update()
	{
		switch (WhichUnicycle)
		{
			case Player.Bike1:
				AccP1();
				IsPlayerOneMoving();
				break;
			case Player.Bike2:
				AccP2();
				IsPlayerTwoMoving();
				break;
			default:
				break;
		}

		ChangeTiers();
		RotateWheel();
		Boost();
	}


	private void FixedUpdate()
	{
		if (ReadyScript.GameStarted)
		{
			Movement();
			Rotation();
		}
	}



	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Collect")
		{
			switch (WhichUnicycle)
			{
				case Player.Bike1:
					transform.localScale += Vector3.one * ScoreScript.Player1Score / 2000;
					GetComponent<FixedJoint>().connectedAnchor = transform.localPosition;
					break;
				case Player.Bike2:
					transform.localScale += Vector3.one * ScoreScript.Player2Score / 2000;
					GetComponent<FixedJoint>().connectedAnchor = transform.localPosition;
					break;
				default:
					break;
			}
		}



		else if (collision.gameObject.tag == "PowerUp")
		{
			switch (WhichUnicycle)
			{
				case Player.Bike1:

					if (collision.gameObject.name == "Rockets")
					{
						RocketPower.SetActive(true);
						HasRockets = true;
					}
					else if (collision.gameObject.name == "Spikes")
					{
						AccelerationSpeedP1 = 6;
						SpikedWheel.SetActive(true);
						Wheel.SetActive(false);
						HasSpikedWheels = true;
					}

					break;
				case Player.Bike2:

					if (collision.gameObject.name == "Rockets")
					{
						RocketPower.SetActive(true);
						HasRockets = true;
					}
					else if (collision.gameObject.name == "Spikes")
					{
						AccelerationSpeedP2 = 6;
						SpikedWheel.SetActive(true);
						Wheel.SetActive(false);
						HasSpikedWheels = true;
					}

					break;
				default:
					break;
			}
		}
	}


	private void Movement()
	{
		float Ver;

		switch (WhichUnicycle)
		{
			case Player.Bike1:
				Ver = Input.GetAxis("VerticalP1");
				RB.velocity = transform.forward * Ver * MoveSpeedP1 * AccelerationSpeedP1;
				break;
			case Player.Bike2:
				Ver = Input.GetAxis("VerticalP2");
				RB.velocity = transform.forward * Ver * MoveSpeedP2 * AccelerationSpeedP2;
				break;
			default:
				break;
		}
	}

	

	private void Rotation()
	{
		float Hoz;
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

		switch (WhichUnicycle)
		{
			case Player.Bike1:

				Hoz = Input.GetAxis("HorizontalP1");
				transform.Rotate(new Vector3(0, Hoz, 0) * Time.deltaTime * 50f);

				if (Hoz == 0)
				{
					Debug.Log("Not Pressed");
					RB.constraints = RigidbodyConstraints.FreezeRotation;
				}

				break;
			case Player.Bike2:

				Hoz = Input.GetAxis("HorizontalP2");
				transform.Rotate(new Vector3(0, Hoz, 0) * Time.deltaTime * 50f);

				if (Hoz == 0)
				{
					Debug.Log("Not Pressed");
					RB.constraints = RigidbodyConstraints.FreezeRotation;
				}

				break;
			default:
				break;
		}
	}


	private bool IsPlayerOneMoving()
	{
		if ((Input.GetAxis("HorizontalP1") <= 0) && (Input.GetAxis("VerticalP1") <= 0))
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	private bool IsPlayerTwoMoving()
	{
		if ((Input.GetAxis("HorizontalP2") <= 0) && (Input.GetAxis("VerticalP2") <= 0))
		{
			return false;
		}
		else
		{
			return true;
		}
	}


	private void RotateWheel()
	{
		switch (WhichUnicycle)
		{
			case Player.Bike1:

				Wheel.GetComponent<RotateScript>().SetRotationSpeed(Input.GetAxis("HorizontalP1") + Input.GetAxis("VerticalP1") * MoveSpeedP1);
				SpikedWheel.GetComponent<RotateScript>().SetRotationSpeed(Input.GetAxis("HorizontalP1") + Input.GetAxis("VerticalP1") * MoveSpeedP1);

				break;

			case Player.Bike2:

				Wheel.GetComponent<RotateScript>().SetRotationSpeed(Input.GetAxis("HorizontalP2") + Input.GetAxis("VerticalP2") * MoveSpeedP2);
				SpikedWheel.GetComponent<RotateScript>().SetRotationSpeed(Input.GetAxis("HorizontalP1") + Input.GetAxis("VerticalP2") * MoveSpeedP2);

				break;
			default:
				break;
		}

	}


	private void AccP1()
	{
		if (!HasSpikedWheels)
		{
			if (Input.GetAxis("P1Acc") > 0)
			{
				if (!PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 5;
					PressedLeftP1 = true;
				}
			}
			else if (Input.GetAxis("P1Acc") < 0)
			{
				if (PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 5;
					PressedLeftP1 = false;
				}
			}
			else
			{
				if (MoveSpeedP1 > 0)
				{
					MoveSpeedP1 -= Time.deltaTime / 4;
				}
			}
		}
		else
		{
			if (Input.GetAxis("P1Acc") > 0)
			{
				if (!PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 10;
					PressedLeftP1 = true;
				}
			}
			else if (Input.GetAxis("P1Acc") < 0)
			{
				if (PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 10;
					PressedLeftP1 = false;
				}
			}
			else
			{
				if (MoveSpeedP1 > 0)
				{
					MoveSpeedP1 -= Time.deltaTime / 4;
				}
			}
		}
	}


	private void AccP2()
	{
		if (!HasSpikedWheels)
		{
			if (Input.GetAxis("P2Acc") > 0)
			{
				if (!PressedLeftP2)
				{
					MoveSpeedP2 += Time.deltaTime * 5;
					PressedLeftP2 = true;
				}
			}
			else if (Input.GetAxis("P2Acc") < 0)
			{
				if (PressedLeftP2)
				{
					MoveSpeedP2 += Time.deltaTime * 5;
					PressedLeftP2 = false;
				}
			}
			else
			{
				if (MoveSpeedP2 > 0)
				{
					MoveSpeedP2 -= Time.deltaTime / 4;
				}
			}
		}
		else
		{
			if (Input.GetAxis("P2Acc") > 0)
			{
				if (!PressedLeftP2)
				{
					MoveSpeedP2 += Time.deltaTime * 10;
					PressedLeftP2 = true;
				}
			}
			else if (Input.GetAxis("P2Acc") < 0)
			{
				if (PressedLeftP2)
				{
					MoveSpeedP2 += Time.deltaTime * 10;
					PressedLeftP2 = false;
				}
			}
			else
			{
				if (MoveSpeedP2 > 0)
				{
					MoveSpeedP2 -= Time.deltaTime / 4;
				}
			}
		}
	}

	
	private void ChangeTiers()
	{
		switch (WhichUnicycle)
		{
			case Player.Bike1:

				if (ScoreScript.Player1Score >= 150)
				{
					P1Tier = Tiers.Tier3;
				}
				else if (ScoreScript.Player1Score >= 50)
				{
					P1Tier = Tiers.Tier2;
				}

				break;
			case Player.Bike2:

				if (ScoreScript.Player2Score >= 150)
				{
					P2Tier = Tiers.Tier3;
				}
				else if (ScoreScript.Player2Score >= 50)
				{
					P2Tier = Tiers.Tier2;
				}

				break;
			default:
				break;
		}
	}


	private void Boost()
	{
		switch (WhichUnicycle)
		{
			case Player.Bike1:

				if (HasRockets)
				{
					if (Input.GetAxis("FireP1") > 0)
					{
						SoundScript.PlaySound("Bang", .5f);
						MoveSpeedP1 = MoveSpeedP1 * 2;
						HasRockets = false;
						RocketPower.SetActive(false);
					}
				}

				break;
			case Player.Bike2:

				if (HasRockets)
				{
					if (Input.GetAxis("FireP2") > 0)
					{
						SoundScript.PlaySound("Bang", .5f);
						MoveSpeedP2 = MoveSpeedP2 * 2;
						HasRockets = false;
						RocketPower.SetActive(false);
					}
				}

				break;
			default:
				break;
		}
	}
}
