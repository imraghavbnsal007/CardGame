using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip flip;
    public AudioClip shuffle;
    public AudioClip thump;
    public AudioClip backgroundMusic;

    public static SFX sfxInstance;

    private void Awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else { 
            sfxInstance = this;
            DontDestroyOnLoad(this);
        
        }

    }
}
