using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollFix : MonoBehaviour
{
	Transform[] children;
	private Vector3[] _connectedAnchor;
	private Vector3[] _anchor;

	void Start()
	{
		children = transform.GetComponentsInChildren<Transform>();
		_connectedAnchor = new Vector3[children.Length];
		_anchor = new Vector3[children.Length];
		for (int i = 0; i < children.Length; i++)
		{
			if (children[i].GetComponent<Joint>() != null)
			{
				_connectedAnchor[i] = children[i].GetComponent<Joint>().connectedAnchor;
				_anchor[i] = children[i].GetComponent<Joint>().anchor;
			}
		}
	}

	private void Update()
	{
		for (int i = 0; i < children.Length; i++)
		{
			if (children[i].GetComponent<Joint>() != null)
			{
				children[i].GetComponent<Joint>().connectedAnchor = _connectedAnchor[i];
				children[i].GetComponent<Joint>().anchor = _anchor[i];
			}
		}
	}
}
