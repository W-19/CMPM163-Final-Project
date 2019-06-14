using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public GameObject root;

    float combined()
    {
        int numPartitions = 1;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2;

        for (int i = 0; i < numDisplayedBins; i++) {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions) {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData[AudioPeer.closestSpeaker][i] / (512 / numPartitions);
            } else {
                partitionIndx++;
                i--;
            }
        }

        for (int i = 0; i < numPartitions; i++) {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100) {
                aveMag[i] = 100;
            }
        }
        return aveMag[0];
    }
    [HideInInspector]
    public float max = 0.54f;
    void Update()
    {
        int numPartitions = 8;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

        for (int i = 0; i < numDisplayedBins; i++) {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions) {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData[AudioPeer.closestSpeaker][i] / (512 / numPartitions);
            } else {
                partitionIndx++;
                i--;
            }
        }

        // scale and bound the average magnitude.
        for (int i = 0; i < numPartitions; i++) {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100) {
                aveMag[i] = 100;
            }
        }

        float mag = combined();
        Color temp = root.GetComponent<PingPong_CellularAutomata>()._Dead;
        if (mag>max&&mag > 0.54) { // gives it a base threshold
            max = mag;
            GetComponent<PingPong_CellularAutomata>().runPixelChange = true;
            
        }
        if (max > 0.54001) {    
            max -= Mathf.Pow(2*max,10) / 5000; // the larger the max is, the quicker this decreases. These values seemed to work fine enough.
        }
        if (aveMag[0] > 0.51) {
            float red = Random.Range(0.0f, 1f);
            Color ColorTemp = new Color(red, temp.g, temp.b, 1);
            root.GetComponent<PingPong_CellularAutomata>()._Dead = Color.Lerp(root.GetComponent<PingPong_CellularAutomata>()._Dead,ColorTemp,0.1f);
        }
        if (aveMag[1] > 0.51) {
            float blue = Random.Range(0.0f, 1f);

            Color ColorTemp = new Color(temp.r, temp.g, blue, 1);
            root.GetComponent<PingPong_CellularAutomata>()._Dead = Color.Lerp(root.GetComponent<PingPong_CellularAutomata>()._Dead, ColorTemp, 0.1f);
        }
        if (aveMag[2] > 0.51) {
            float green = Random.Range(0.0f, 1f);

            Color ColorTemp = new Color(temp.r, green, temp.b, 1);
            root.GetComponent<PingPong_CellularAutomata>()._Dead = Color.Lerp(root.GetComponent<PingPong_CellularAutomata>()._Dead, ColorTemp, 0.1f);
        }

        //Debug.Log(mag);
    }
}
