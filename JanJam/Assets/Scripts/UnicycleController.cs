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
				AccP2();
				IsPlayerTwoMoving();
				break;
			default:
				break;
		}

		Movement();
		Rotation();
		RotateWheel();
	}



	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Collect")
		{
			switch (WhichUnicycle)
			{
				case Player.Bike1:
					ScoreScript.Player1Scored();
					transform.localScale += Vector3.one * ScoreScript.Player1Score / 20;
					break;
				case Player.Bike2:
					ScoreScript.Player2Scored();
					transform.localScale += Vector3.one * ScoreScript.Player2Score / 20;
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

		switch (WhichUnicycle)
		{
			case Player.Bike1:
				Hoz = Input.GetAxis("HorizontalP1");
				RB.velocity += transform.right * Hoz * MoveSpeedP1;
				TS.rotation *= Quaternion.Euler(transform.worldToLocalMatrix.MultiplyVector(transform.up) * Hoz * 4);
				break;
			case Player.Bike2:
				Hoz = Input.GetAxis("HorizontalP2");
				RB.velocity += transform.right * Hoz * MoveSpeedP2;
				TS.rotation *= Quaternion.Euler(transform.worldToLocalMatrix.MultiplyVector(transform.up) * Hoz * MoveSpeedP2);
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

				break;

			case Player.Bike2:

				Wheel.GetComponent<RotateScript>().SetRotationSpeed(Input.GetAxis("HorizontalP2") + Input.GetAxis("VerticalP2") * MoveSpeedP2);

				break;
			default:
				break;
		}

	}


	private void AccP1()
	{
		if (Input.GetAxis("P1Acc") > 0)
		{
			MoveSpeedP1 += Time.deltaTime * 5;
		}
		else
		{
			if (MoveSpeedP1 > 0)
			{
				MoveSpeedP1 -= Time.deltaTime;
			}
		}

		//transform.Rotate(0, 0, Input.GetAxis("P1Acc"));

	}


	private void AccP2()
	{
		if (Input.GetAxis("P2Acc") > 0)
		{
			MoveSpeedP2 += Time.deltaTime * 5;
		}
		else
		{
			if (MoveSpeedP2 > 0)
			{
				MoveSpeedP2 -= Time.deltaTime;
			}
		}

		//transform.Rotate(0, 0, Input.GetAxis("P1Acc"));

	}


	private void CanEatOther()
	{

	}
}
