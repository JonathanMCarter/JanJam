using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{

	public Image P1Indicator;
	public Image P2Indicator;

	public GameObject P1;
	public GameObject P2;

	public Sprite[] Trees;
	private TierScript Tier;

	private void Update()
	{
		if (P1.GetComponent<UnicycleController>().P1Tier == Tiers.Tier1)
		{
			P1Indicator.sprite = Trees[0];
		}
		else if (P1.GetComponent<UnicycleController>().P1Tier == Tiers.Tier2)
		{
			P1Indicator.sprite = Trees[1];
		}
		else if (P1.GetComponent<UnicycleController>().P1Tier == Tiers.Tier3)
		{
			P1Indicator.sprite = Trees[2];
		}

		if (P2.GetComponent<UnicycleController>().P2Tier == Tiers.Tier1)
		{
			P2Indicator.sprite = Trees[0];
		}
		else if (P2.GetComponent<UnicycleController>().P2Tier == Tiers.Tier2)
		{
			P2Indicator.sprite = Trees[1];
		}
		else if (P2.GetComponent<UnicycleController>().P2Tier == Tiers.Tier3)
		{
			P2Indicator.sprite = Trees[2];
		}
	}
}
