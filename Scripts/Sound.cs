using UnityEngine;
using UnityEngine.Audio;
using System;

[CreateAssetMenu(menuName = "EzAudioManager/Sound")]

public class Sound : ScriptableObject
{
    [HideInInspector]

    public AudioSource source;

    [Space(5)]

    [Header("Main Settings")]

    public AudioClip[] clips;

    public AudioMixerGroup mixerGroup;

    public string customParentName;

    [Header("Audio Properties")]

    public bool playOnStart;
    
    public bool loop;

    [Range(0.0f, 1.0f)] public float volume = 1.0f;

    [Range(0.001f, 3.0f)] public float minPitch = 1.0f, maxPitch = 1.0f;

    [Header("Fade Effect")]

    public bool fadeEffectEnabled;

    public AnimationCurve fadeInCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public AnimationCurve fadeOutCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);

    [Header("Low Pass Filter")]

    public bool lowPassEnabled;

    [Range(10.0f, 22000.0f)] public float lowPassFrequency = 5000.0f;

    [Range(1.0f, 10.0f)] public float lowPassResonance = 1.0f;

    [Header("High Pass Filter")]

    public bool highPassEnabled;

    [Range(10.0f, 22000.0f)] public float highPassFrequency = 5000.0f;

    [Range(1.0f, 10.0f)] public float highPassResonance = 1.0f;

    [Header("Distortion Filter")]

    public bool distortionEnabled;

    [Range(0.0f, 1.0f)] public float distortionLevel = 0.5f;

    [Header("3D Settings")]

    public bool enableSpatialSound;

    [Range(0.0f, 5.0f)] public float dopplerLevel = 1.0f;

    [Range(0.0f, 360.0f)] public float spread = 0.0f;

    public AudioRolloffMode rolloffMode;

    public float minDistance = 1.0f, maxDistance = 500.0f;
}
