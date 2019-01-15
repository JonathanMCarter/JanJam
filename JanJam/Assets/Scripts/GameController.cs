using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
	Menu,
	Level,
	End,
};


public class GameController : MonoBehaviour
{

	public Scenes Scene;


	private void Awake()
	{
		DontDestroyOnLoad(this);
	}


	internal void ChangeScene(Scenes Selection)
	{
		switch (Selection)
		{
			case Scenes.Menu:
				SceneManager.LoadSceneAsync(0);
				break;
			case Scenes.Level:
				SceneManager.LoadSceneAsync(1);
				break;
			case Scenes.End:
				SceneManager.LoadSceneAsync(2);
				break;
			default:
				break;
		}
	}
}
