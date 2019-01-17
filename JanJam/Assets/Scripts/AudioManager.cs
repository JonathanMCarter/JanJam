using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public bool SceneChanged;                                                                   // Bool for whether the scene has changed or not
	public GameObject Sound_Prefab;                                                             // Holds the prefab that plays the sound requested
	public List<string> Sound_Names = new List<string>();                                       // A list to hold the audioclip names
	public List<AudioClip> Sound_Clips = new List<AudioClip>();                                 // A list to hold the audioclips themselves
	public Dictionary<string, AudioClip> Sound_Lib = new Dictionary<string, AudioClip>();       // Dictionary that holds the audio names and clips


	private void Start()
	{
		for (int i = 0; i < Sound_Names.Count; i++)         // For loop that populates the dictionary with all the sound assets in the lists
		{
			Sound_Lib.Add(Sound_Names[i], Sound_Clips[i]);
		}
	}


	public void PlaySound(string request)                   // Fuction to select and play a sound asset from the start and destroy the prefab once it has played
	{
		if (Sound_Lib.ContainsKey(request))                                 // If the sound is in the library
		{
			GameObject clip = Instantiate(Sound_Prefab);                        // Instantiate a Sound prefab
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];         // Get the prefab and add the requested clip to it
			clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
			ChangeVolume(1);                                                    // reset the volume if it was altered
		}
	}



	public void PlaySound(string request, float loudness)                   // Fuction to select and play a sound asset from the start and destroy the prefab once it has played
	{
		if (Sound_Lib.ContainsKey(request))                                 // If the sound is in the library
		{
			GameObject clip = Instantiate(Sound_Prefab);                        // Instantiate a Sound prefab
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];         // Get the prefab and add the requested clip to it
			clip.GetComponent<AudioSource>().volume = loudness;
			clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
			ChangeVolume(1);                                                    // reset the volume if it was altered
		}
	}


	public void PlaySound(string request, float loudness, float pitchchange)                   // Fuction to select and play a sound asset from the start and destroy the prefab once it has played
	{
		if (Sound_Lib.ContainsKey(request))                                 // If the sound is in the library
		{
			GameObject clip = Instantiate(Sound_Prefab);                        // Instantiate a Sound prefab
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];         // Get the prefab and add the requested clip to it
			clip.GetComponent<AudioSource>().volume = loudness;
			clip.GetComponent<AudioSource>().pitch = pitchchange;
			clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
			ChangeVolume(1);                                                    // reset the volume if it was altered
		}
	}



	public void PlayFromTime(string request, float time)    // Function the play audio from a certain time (most is the same as PlaySound() )
	{
		if (Sound_Lib.ContainsKey(request))
		{
			GameObject clip = Instantiate(Sound_Prefab);
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];
			clip.GetComponent<AudioSource>().time = time;                       // Only difference, get a time input and set the clip to be at the time
			clip.GetComponent<AudioSource>().Play();
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
			ChangeVolume(1);
		}
	}


	public void PlayFromTime(string request, float time, float loudness)    // Function the play audio from a certain time (most is the same as PlaySound() )
	{
		if (Sound_Lib.ContainsKey(request))
		{
			GameObject clip = Instantiate(Sound_Prefab);
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];
			clip.GetComponent<AudioSource>().time = time;                       // Only difference, get a time input and set the clip to be at the time
			clip.GetComponent<AudioSource>().volume = loudness;
			clip.GetComponent<AudioSource>().Play();
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
			ChangeVolume(1);
		}
	}



	public void PlayFromTime(string request, float time, float loudness, float pitchchange)    // Function the play audio from a certain time (most is the same as PlaySound() )
	{
		if (Sound_Lib.ContainsKey(request))
		{
			GameObject clip = Instantiate(Sound_Prefab);
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];
			clip.GetComponent<AudioSource>().time = time;                       // Only difference, get a time input and set the clip to be at the time
			clip.GetComponent<AudioSource>().volume = loudness;
			clip.GetComponent<AudioSource>().pitch = pitchchange;
			clip.GetComponent<AudioSource>().Play();
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
			ChangeVolume(1);
		}
	}



	public void PlayWithDelay(string request, float delay)  // Function that plays sound with a delay (most is the same as PlaySound() )
	{
		if (Sound_Lib.ContainsKey(request))
		{
			GameObject clip = Instantiate(Sound_Prefab);
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];
			clip.GetComponent<AudioSource>().PlayDelayed(delay);                // Only difference, played with a delay rather that right away
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
			ChangeVolume(1);
		}
	}

	public void PlayWithDelay(string request, float delay, float loudness)  // Function that plays sound with a delay (most is the same as PlaySound() )
	{
		if (Sound_Lib.ContainsKey(request))
		{
			GameObject clip = Instantiate(Sound_Prefab);
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];
			clip.GetComponent<AudioSource>().volume = loudness;
			clip.GetComponent<AudioSource>().PlayDelayed(delay);                // Only difference, played with a delay rather that right away
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
			ChangeVolume(1);
		}
	}


	public void ChangeVolume(float input)       // Function the change the clips volume
	{
		GameObject clip = Sound_Prefab;                         // gets the sound prefab	
		clip.GetComponent<AudioSource>().volume = input;        // adjusts the volume to the input
	}
}