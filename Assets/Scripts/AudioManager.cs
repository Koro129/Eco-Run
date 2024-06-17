using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioClip background;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}