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

	private Rigidbody RB;
	private Transform TS;

	[Header("Player1")]
	public float MoveSpeedP1;
	public float MinSpeedP1 = 3f;
	public float MaxSpeedP1 = 10f;
	public float AccelerationSpeedP1 = .25f;

	[Header("Player2")]
	public float MoveSpeedP2;
	public float MinSpeedP2 = 3f;
	public float MaxSpeedP2 = 10f;
	public float AccelerationSpeedP2 = .25f;

	public Quaternion Rot;

	private ScoringScript ScoreScript;


	void Start()
	{
		RB = GetComponent<Rigidbody>();
		TS = GetComponent<Transform>();
		Rot = new Quaternion(0, 0, 10 * Time.deltaTime, 0);
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
				IsPlayerTwoMoving();
				break;
			default:
				break;
		}

		Movement();
		Rotation();
		RotateWheel();
	}



	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Collect")
		{
			switch (WhichUnicycle)
			{
				case Player.Bike1:
					ScoreScript.Player1Scored();
					break;
				case Player.Bike2:
					ScoreScript.Player2Scored();
					break;
				default:
					break;
			}

			Destroy(collision.gameObject);
		}
	}


	private void Movement()
	{
		float Ver;

		switch (WhichUnicycle)
		{
			case Player.Bike1:
				Ver = Input.GetAxis("VerticalP1");
				RB.velocity = transform.forward * Ver * MoveSpeedP1;
				break;
			case Player.Bike2:
				Ver = Input.GetAxis("VerticalP2");
				RB.velocity = transform.forward * Ver * MoveSpeedP2;
				break;
			default:
				break;
		}
	}



	private void Rotation()
	{
		float Hoz;

		switch (WhichUnicycle)
		{
			case Player.Bike1:
				Hoz = Input.GetAxis("HorizontalP1");
				TS.rotation *= Quaternion.Euler(Vector3.up * Hoz * MoveSpeedP1);
				break;
			case Player.Bike2:
				Hoz = Input.GetAxis("HorizontalP2");
				TS.rotation *= Quaternion.Euler(Vector3.up * Hoz * MoveSpeedP2);
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

				if (IsPlayerOneMoving())
				{
					Wheel.GetComponent<RotateScript>().SetRotationSpeed(MoveSpeedP1);
				}
				else
				{
					Wheel.GetComponent<RotateScript>().SetRotationSpeed(0f);
				}

				break;

			case Player.Bike2:
				if (IsPlayerTwoMoving())
				{
					Wheel.GetComponent<RotateScript>().SetRotationSpeed(MoveSpeedP2);
				}
				else
				{
					Wheel.GetComponent<RotateScript>().SetRotationSpeed(0f);
				}
				break;
			default:
				break;
		}

	}

	private void AccP1()
	{
		MoveSpeedP1 = Input.GetAxis("VerticalP1") * AccelerationSpeedP1;
	}
}
