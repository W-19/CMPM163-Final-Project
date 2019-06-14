// Based off the script from David's Week 8 Section
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleColorScript: MonoBehaviour {

    ParticleSystem ps;

    private ParticleSystem.ColorOverLifetimeModule col;
    private ParticleSystem.MainModule settings;
    
    private int frames = 0;
    private Color c;
    private Gradient g;

    void Start () 
    {
	    // Get Particle system component
	    ps = GetComponent<ParticleSystem>();
	    settings = ps.main; // The main module with things like start color, start speed, etc.

	    // Enable the Color Over Lifetime module if it's not already
        col = ps.colorOverLifetime;
        col.enabled = true;

        g = new Gradient();
	}

	void Update () {

		// This code will only execute every 10 frames
		frames++;
		if(frames % 10 != 0) return;
    
		int numPartitions = 256;
		float[] aveMag = new float[numPartitions];
		float partitionIndx = 0;
		int numDisplayedBins = 512 / 2; 

		for (int i = 0; i < numDisplayedBins; i++) 
		{
			if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions){
				aveMag[(int)partitionIndx] += AudioPeer.spectrumData[AudioPeer.closestSpeaker][i] / (512/numPartitions);
			}
			else{
				partitionIndx++;
				i--;
			}
		}

		for(int i = 0; i < 32; i++) // Let's only deal with the first 32 partitions
		{
			aveMag[i] = aveMag[i]*100;
			if (aveMag[i] > 100) {
				aveMag[i] = 100;
			}

			
			c = Color.Lerp(Color.red, Color.yellow, (float)i/32); // For now let's just make the particle one color for its entire lifetime
			
			// No idea why the below code works for alpha but not for color
			/*
			g.SetKeys(
					new GradientColorKey[] {new GradientColorKey(c, 0.0f), new GradientColorKey(c, 1.0f)},
					new GradientAlphaKey[] {new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.9f), new GradientAlphaKey(0.0f, 1.0f)}
			);
			col.color = g;
			*/
			
     		settings.startColor = new ParticleSystem.MinMaxGradient(Color.Lerp(Color.red, Color.yellow, (float)i/32));

			ps.Emit((int)Math.Floor(aveMag[i]*3)); // For each bin, emit 0 to 3 particles... at least that's what I'm trying to do
			
		}

	}


}