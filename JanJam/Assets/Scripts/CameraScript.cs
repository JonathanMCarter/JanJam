using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	public GameObject CameraPoint;

	private Vector3 Offset;



	private void Start()
	{
		Offset = transform.position - CameraPoint.transform.position;
	}

	void LateUpdate()
    {
		transform.position = CameraPoint.transform.position + Offset;
    }
}
