using System;
using FrankenToilet.flazhik.Assets;
using UnityEngine;

namespace FrankenToilet.flazhik.Components;

public sealed class BigSmokeDeath : MonoBehaviour
{
    [ExternalAsset("Assets/UltrakillSanAndreas/Sounds/Oooh.mp3", typeof(AudioClip))]
    private static AudioClip oooh;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = oooh;
        audioSource.outputAudioMixerGroup = AudioMixerController.Instance.allGroup;
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
            Destroy(gameObject);
    }
}