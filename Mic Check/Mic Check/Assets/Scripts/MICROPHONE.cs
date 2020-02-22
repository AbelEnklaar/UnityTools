using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MICROPHONE : MonoBehaviour
{
    private AudioSource micTake;
    private string ActiveMic;

    public float[] samples = new float [512];
    void Start()
    {
        //look for connected microphone and print their name in the console
       micTake = GetComponent<AudioSource>();
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        // Start recording with built-in Microphone and play the recorded audio right away

        //check if there isn't a microphone connected, and take the first microphone and start it as audiosource micTake 
        if (ActiveMic == null)
        {
            ActiveMic = Microphone.devices[0];
            Debug.Log(ActiveMic);
            micTake.clip = Microphone.Start(ActiveMic, true, 100, 44100);
            while (!(Microphone.GetPosition(ActiveMic) > 0))
            {
            }
            
            micTake.Play();

        }
    }

   
}
