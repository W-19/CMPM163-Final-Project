using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create this required component....
[RequireComponent (typeof (AudioSource))]

public class AudioPeer : MonoBehaviour {

	// need to instantiate an audio source and array of samples to store the fft data.

	public Transform _speakersGroupObj;
	//private AudioSource[] _speakers;

	// NOTE: make this a 'static' float so we can access it from any other script.
	public static float[][] spectrumData = new float[4][];
	public static int closestSpeaker = 0;

	// Use this for initialization
	void Start () {
		
		//_speakers = new AudioSource[_speakersGroup.childCount];
		for(int i = 0; i < _speakersGroupObj.childCount; i++){
			spectrumData[i] = new float[512];
		}
		//_audioSource = GetComponent<AudioSource> ();	

	}


	// Update is called once per frame
	void Update () {

		double distanceToClosestSpeaker = 1337.1337;
		for(int i = 0; i < _speakersGroupObj.childCount; i++){		
			// Here's where we assume the script is attached to the player
			if(Vector3.Distance(transform.position, _speakersGroupObj.GetChild(i).position) < distanceToClosestSpeaker){
				distanceToClosestSpeaker = Vector3.Distance(transform.position, _speakersGroupObj.GetChild(i).position);
				closestSpeaker = i;
			}

			// this method computes the fft of the audio data, and then populates spectrumData with the spectrum data.
			_speakersGroupObj.GetChild(i).gameObject.GetComponent<AudioSource>().GetSpectrumData (spectrumData[i], 0, FFTWindow.Hanning);
		}

	}

}


