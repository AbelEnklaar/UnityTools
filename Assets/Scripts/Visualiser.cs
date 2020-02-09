using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualiser : MonoBehaviour
{
    public float loudness = 0;
    public int samples = 512;
    public AudioSource audio;
    public Slider slider;
    [Range(1, 100)] public float normalizer = 1f;

    // Update is called once per frame
    void Update()
    {
        loudness = GetAverageVolume() * normalizer;
        slider.value = loudness; 
    }

    float GetAverageVolume() 
        {
            float[] data = new float[samples];
            float a = 0f;
            audio.GetOutputData(data, 0);
            foreach (var s in data)
            {
                a += Mathf.Abs(s);
            }

            return a / samples;
            
        }
        
    }

