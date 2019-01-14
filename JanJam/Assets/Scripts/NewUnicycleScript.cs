using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUnicycleScript : MonoBehaviour
{

	public float MoveSpeed;
	public Quaternion Rot;

	private void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			GetComponent<Rigidbody>().velocity += new Vector3(-.5f, 0, 1) * MoveSpeed * Time.deltaTime;
			transform.Rotate(0, 0, -.5f * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			GetComponent<Rigidbody>().velocity += new Vector3(.5f, 0, 1) * MoveSpeed * Time.deltaTime;
			transform.Rotate(0, 0, .5f * Time.deltaTime);
		}
	}
}
