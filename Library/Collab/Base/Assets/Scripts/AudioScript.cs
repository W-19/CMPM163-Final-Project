using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public GameObject root;
    // Start is called before the first frame update
    void Start()
    {
        //root = GetComponent<GameObject>();
    }


    void Update()
    {
        int numPartitions = 8;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

        for (int i = 0; i < numDisplayedBins; i++) {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions) {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData[i] / (512 / numPartitions);
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

        float mag = aveMag[0];
        Color temp = root.GetComponent<PingPong_CellularAutomata>()._Dead;
        if (Input.GetKeyDown("e")) {
            Debug.Log("e");
            GetComponent<PingPong_CellularAutomata>().runPixelChange = true;
            
        }
        if (aveMag[0] > 0.51) {
            //root.GetComponent<PingPong_CellularAutomata>().pixelChangeFunction();
            float red = Random.Range(0.0f, 1f);
            GetComponent<PingPong_CellularAutomata>().runPixelChange = true;
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
