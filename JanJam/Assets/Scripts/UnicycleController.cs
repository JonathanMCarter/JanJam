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

	private ScoringScript ScoreScript;


	void Start()
	{
		RB = GetComponent<Rigidbody>();
		TS = GetComponent<Transform>();
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();
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
	}


	private void FixedUpdate()
	{
		Movement();
		Rotation();
	}



	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Collect")
		{
			switch (WhichUnicycle)
			{
				case Player.Bike1:

					switch (collision.gameObject.GetComponent<TierScript>().CollectTier)
					{
						case Tiers.Tier1:
							ScoreScript.Player1Scored(1);
							break;
						case Tiers.Tier2:
							ScoreScript.Player1Scored(2);
							break;
						case Tiers.Tier3:
							ScoreScript.Player1Scored(4);
							break;
						case Tiers.Tier4:
							ScoreScript.Player1Scored(8);
							break;
						case Tiers.Tier5:
							ScoreScript.Player1Scored(16);
							break;
						default:
							break;
					}

					transform.localScale += Vector3.one * ScoreScript.Player1Score / 2000;
					GetComponent<FixedJoint>().connectedAnchor = transform.localPosition;
					break;
				case Player.Bike2:

					switch (collision.gameObject.GetComponent<TierScript>().CollectTier)
					{
						case Tiers.Tier1:
							ScoreScript.Player2Scored(1);
							break;
						case Tiers.Tier2:
							ScoreScript.Player2Scored(2);
							break;
						case Tiers.Tier3:
							ScoreScript.Player2Scored(4);
							break;
						case Tiers.Tier4:
							ScoreScript.Player2Scored(8);
							break;
						case Tiers.Tier5:
							ScoreScript.Player2Scored(16);
							break;
						default:
							break;
					}

					transform.localScale += Vector3.one * ScoreScript.Player2Score / 2000;
					GetComponent<FixedJoint>().connectedAnchor = transform.localPosition;
					break;
				default:
					break;
			}

			Destroy(collision.gameObject);
		}



		else if (collision.gameObject.tag == "PowerUp")
		{
			switch (WhichUnicycle)
			{
				case Player.Bike1:
					
					if (collision.gameObject.name == "Rockets")
					{
						RocketPower.SetActive(true);
					}
					else if (collision.gameObject.name == "Spikes")
					{
						AccelerationSpeedP1 = 6;
						SpikedWheel.SetActive(true);
						Wheel.SetActive(false);
					}

					break;
				case Player.Bike2:

					if (collision.gameObject.name == "Rockets")
					{
						RocketPower.SetActive(true);
					}
					else if (collision.gameObject.name == "Spikes")
					{
						AccelerationSpeedP2 = 6;
						SpikedWheel.SetActive(true);
						Wheel.SetActive(false);
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
			if (Input.GetKeyDown(KeyCode.Q))
			{
				if (!PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 5;
					PressedLeftP1 = true;
				}
			}
			else if (Input.GetKeyDown(KeyCode.E))
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
			if (Input.GetKeyDown(KeyCode.Q))
			{
				if (!PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 10;
					PressedLeftP1 = true;
				}
			}
			else if (Input.GetKeyDown(KeyCode.E))
			{
				if (PressedLeftP1)
				{
					MoveSpeedP1 += Time.deltaTime * 10;
					PressedLeftP1 = false;
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


	private void AccP2()
	{
		if (!HasSpikedWheels)
		{
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				if (!PressedLeftP2)
				{
					MoveSpeedP2 += Time.deltaTime * 5;
					PressedLeftP2 = true;
				}
			}
			else if (Input.GetKeyDown(KeyCode.Keypad6))
			{
				if (PressedLeftP2)
				{
					MoveSpeedP2 -= Time.deltaTime;
					PressedLeftP2 = false;
				}
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				if (!PressedLeftP2)
				{
					MoveSpeedP2 += Time.deltaTime * 10;
					PressedLeftP2 = true;
				}
			}
			else if (Input.GetKeyDown(KeyCode.Keypad6))
			{
				if (PressedLeftP2)
				{
					MoveSpeedP2 -= Time.deltaTime;
					PressedLeftP2 = false;
				}
			}
		}
	}

	
	private void ChangeTiers()
	{
		switch (WhichUnicycle)
		{
			case Player.Bike1:

				if (ScoreScript.Player1Score >= 100)
				{
					P1Tier = Tiers.Tier2;
				}
				else if (ScoreScript.Player1Score >= 200)
				{
					P1Tier = Tiers.Tier3;
				}
				else if (ScoreScript.Player1Score >= 500)
				{
					P1Tier = Tiers.Tier4;
				}
				else if (ScoreScript.Player1Score >= 1000)
				{
					P1Tier = Tiers.Tier5;
				}

				break;
			case Player.Bike2:

				if (ScoreScript.Player2Score >= 100)
				{
					P2Tier = Tiers.Tier2;
				}
				else if (ScoreScript.Player2Score >= 200)
				{
					P2Tier = Tiers.Tier3;
				}
				else if (ScoreScript.Player2Score >= 500)
				{
					P2Tier = Tiers.Tier4;
				}
				else if (ScoreScript.Player2Score >= 1000)
				{
					P2Tier = Tiers.Tier5;
				}

				break;
			default:
				break;
		}
	}
}
