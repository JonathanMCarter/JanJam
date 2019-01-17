using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

	private ParticleSystem Particles;
	private MeshRenderer Mesh;
	private AudioManager SoundScript;
	private MeshCollider MeshCollider;
	private ScoringScript ScoreScript;


	private void Start()
	{
		SoundScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManager>();
		Particles = GetComponent<ParticleSystem>();
		Mesh = GetComponent<MeshRenderer>();
		MeshCollider = GetComponent<MeshCollider>();
		ScoreScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoringScript>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bike1")
		{
			if (other.gameObject.GetComponentInParent<UnicycleController>().P1Tier >= GetComponent<TierScript>().CollectTier)
			{
				SoundScript.PlaySound("Collect", .5f, .5f);
				Mesh.enabled = false;
				MeshCollider.enabled = false;

				switch (GetComponent<TierScript>().CollectTier)
				{
					case Tiers.Tier1:
						ScoreScript.Player1Scored(1);
						break;
					case Tiers.Tier2:
						ScoreScript.Player1Scored(2);
						break;
					case Tiers.Tier3:
						ScoreScript.Player1Scored(3);
						break;
					case Tiers.Tier4:
						ScoreScript.Player1Scored(4);
						break;
					case Tiers.Tier5:
						ScoreScript.Player1Scored(5);
						break;
					default:
						break;
				}

				Particles.Play();
				Destroy(gameObject, Particles.main.duration);
			}
		}
		else if (other.gameObject.tag == "Bike2")
		{
			if (other.gameObject.GetComponentInParent<UnicycleController>().P2Tier >= GetComponent<TierScript>().CollectTier)
			{
				SoundScript.PlaySound("Collect", .5f);
				Mesh.enabled = false;
				MeshCollider.enabled = false;

				switch (GetComponent<TierScript>().CollectTier)
				{
					case Tiers.Tier1:
						ScoreScript.Player2Scored(1);
						break;
					case Tiers.Tier2:
						ScoreScript.Player2Scored(2);
						break;
					case Tiers.Tier3:
						ScoreScript.Player2Scored(3);
						break;
					case Tiers.Tier4:
						ScoreScript.Player2Scored(4);
						break;
					case Tiers.Tier5:
						ScoreScript.Player2Scored(5);
						break;
					default:
						break;
				}

				Particles.Play();
				Destroy(gameObject, Particles.main.duration);
			}
		}
	}
}
