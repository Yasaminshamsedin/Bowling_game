using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
  public AudioClip backgroundMusic; 
    private AudioSource audioSource;
    private bool isPlaying = false;

    public Image buttonImage; 
    public Sprite playSprite; 
    public Sprite stopSprite;

   void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = backgroundMusic; 
        audioSource.loop = true; 
        PlayMusic(); 
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        audioSource.Play(); 
        isPlaying = true; 
        buttonImage.sprite = playSprite;
    }

    private void StopMusic()
    {
        audioSource.Stop(); 
        isPlaying = false; 
        buttonImage.sprite = stopSprite;
    }
}

