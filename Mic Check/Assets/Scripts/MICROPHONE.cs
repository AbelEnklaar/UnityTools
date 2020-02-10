using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MICROPHONE : MonoBehaviour
{
    private AudioSource micTake;
    private string ActiveMic;

    public float[] samples = new float [512]; 
    
    // Start is called before the first frame update
    void Start()
    {
       micTake = GetComponent<AudioSource>();
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        // Start recording with built-in Microphone and play the recorded audio right away

        
        if (ActiveMic == null)
        {
            ActiveMic = Microphone.devices[0];
            micTake.clip = Microphone.Start(ActiveMic, true, 100, 44100);
            while (!(Microphone.GetPosition(ActiveMic) > 0))
            {
            }

            micTake.Play();

        }
    }

    // Update is called once per frame
    void Update()
    {
         
        
    }
}
