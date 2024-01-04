using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Play(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void Update()
    {
        if (!audioSource.isPlaying)
        {
            SoundManager.instance.ReturnPool(this.gameObject);
        }
    }
}
