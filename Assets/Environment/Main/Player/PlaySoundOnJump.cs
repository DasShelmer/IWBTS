using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnJump : MonoBehaviour {
    
    AudioSource aS;
    public AudioClip[] acs;

    private void Awake()
    {
        aS = GetComponents<AudioSource>()[1];

        if (acs.Length == 0)
            Debug.LogError("Audio Clip array is empty!", gameObject);
    }

    public void Play()
    {
        if (acs.Length != 0)
        {
            aS.clip = acs[Random.Range(0, acs.Length)];
            aS.Play();
        }
    } 
}
