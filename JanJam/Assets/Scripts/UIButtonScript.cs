using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonScript : MonoBehaviour
{
	public Scenes Selection;
	private GameController Controller;


    void Start()
    {
		Controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


	public void ChangeScene()
	{
		Controller.ChangeScene(Selection);
	}

	public void Reset()
	{
		Controller.gameObject.GetComponent<ScoringScript>().ResetScript();
	}
}
