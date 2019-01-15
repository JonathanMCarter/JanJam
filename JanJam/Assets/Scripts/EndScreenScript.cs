using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour
{

	public GameObject PurpleUni;
	public GameObject RedUni;

	public Text HeadlineText;
	public Text ArticleTextPT2;

	private ScoringScript ScoreScript;

    void Start()
    {
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();

		if (ScoreScript.Player1Score > ScoreScript.Player2Score)
		{
			PurpleUni.SetActive(true);
			HeadlineText.text = "Purple Unicycle Takes Over!";
			ArticleTextPT2.text = "Religious figures have praised the unicycle as their new god. The general public have been buying out unicycles to appease the beast with some success, as long as the unicycle was purple.";
		}
		else if (ScoreScript.Player1Score < ScoreScript.Player2Score)
		{
			RedUni.SetActive(true);
			HeadlineText.text = "Red Unicycle Takes Over!";
			ArticleTextPT2.text = "Religious figures have praised the unicycle as their new god. The general public have been buying out unicycles to appease the beast with some success, as long as the unicycle was red.";
		}
    }

}
