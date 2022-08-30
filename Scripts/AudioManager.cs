using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds = new List<Sound>();

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        ConfigureAudioSources();
    }
    private void ConfigureAudioSources()
    {
        if (sounds.Count == 0)
            return;

        foreach (Sound s in sounds)
        {
            if (s == null)
            {
                return;
            }

            GameObject newAudioObject = new GameObject(s.name);
            if (s.customParentName != null && s.customParentName != "")
            {
                if (GameObject.Find(s.customParentName) != null)
                    newAudioObject.transform.parent = GameObject.Find(s.customParentName).transform;
                else
                {
                    Debug.LogWarning("No object found in scene called: '" + s.customParentName + "'.");
                    newAudioObject.transform.parent = transform;
                }
            }
            else
                newAudioObject.transform.parent = transform;

            s.source = newAudioObject.AddComponent<AudioSource>();

            if (s.clips.Length == 0)
                Debug.LogWarning(s.name + " has no clip(s) selected.");
            else
                s.source.clip = s.clips[0];

            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.spatialBlend = Convert.ToInt32(s.enableSpatialSound);
            s.source.dopplerLevel = s.dopplerLevel;
            s.source.spread = s.spread;
            s.source.rolloffMode = s.rolloffMode;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;

            if (s.fadeEffectEnabled)
            {
                AudioFadeEffect audioFadeEffect = newAudioObject.AddComponent<AudioFadeEffect>();
                audioFadeEffect.sound = s;

            }

            if (s.lowPassEnabled)
            {
                AudioLowPassFilter lowPassFilter = newAudioObject.AddComponent<AudioLowPassFilter>();
                lowPassFilter.cutoffFrequency = s.lowPassFrequency;
                lowPassFilter.lowpassResonanceQ = s.lowPassResonance;
            }

            if (s.highPassEnabled)
            {
                AudioHighPassFilter highPassFilter = newAudioObject.AddComponent<AudioHighPassFilter>();
                highPassFilter.cutoffFrequency = s.highPassFrequency;
                highPassFilter.highpassResonanceQ = s.highPassResonance;
            }

            if (s.distortionEnabled)
            {
                AudioDistortionFilter distortionFilter = newAudioObject.AddComponent<AudioDistortionFilter>();
                distortionFilter.distortionLevel = s.distortionLevel;
            }

            if (s.playOnStart)
                Play(s.name);
        }
    }

    public static void Play(string name)
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot play sound.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s == null)
                break;

            if (s.clips.Length == 0)
            {
                Debug.LogWarning(s.name + " has no clip(s) selected. Cannot play sound.");
                return;
            }

            if (s.name == name)
            {
                s.source.clip = s.clips[Random.Range(0, s.clips.Length)];
                s.source.pitch = Random.Range(s.minPitch, s.maxPitch);
                if (s.fadeEffectEnabled && s.source.GetComponent<AudioFadeEffect>() != null)
                    instance.StartCoroutine(s.source.GetComponent<AudioFadeEffect>().StartFadeIn());
                s.source.Play();
                return;
            }
        }

        Debug.LogWarning("Sound: '" + name + "' not found. Cannot play sound.");
    }

    public static void Stop(string name)
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot stop sound.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s == null)
                break;

            if (s.clips.Length == 0)
            {
                Debug.LogWarning(s.name + " has no clip(s) selected. Cannot stop sound.");
                return;
            }

            if (s.fadeEffectEnabled)
            {
                instance.StartCoroutine(s.source.GetComponent<AudioFadeEffect>().StartFadeOut());
                return;
            }

            if (s.name == name)
            {
                s.source.Stop();
                return;
            }
        }

        Debug.LogWarning("Sound: '" + name + "' not found. Cannot stop sound.");
    }

    public static void Pause(string name)
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot pause sound.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s == null)
                break;

            if (s.clips.Length == 0)
            {
                Debug.LogWarning(s.name + " has no clip(s) selected. Cannot pause sound.");
                return;
            }

            if (s.name == name)
            {
                s.source.Pause();
                return;
            }
        }

        Debug.LogWarning("Sound: '" + name + "' not found. Cannot pause sound.");
    }

    public static void UnPause(string name)
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot unpause sound.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s == null)
                break;

            if (s.clips.Length == 0)
            {
                Debug.LogWarning(s.name + " has no clip(s) selected. Cannot unpause sound.");
                return;
            }

            if (s.name == name)
            {
                s.source.UnPause();
                return;
            }
        }

        Debug.LogWarning("Sound: '" + name + "' not found. Cannot unpause sound.");
    }

    public static void Mute(string name)
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot mute sound.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s == null)
                break;

            if (s.clips.Length == 0)
            {
                Debug.LogWarning(s.name + " has no clip(s) selected. Cannot mute sound.");
                return;
            }

            if (s.name == name)
            {
                s.source.mute = true;
                return;
            }
        }

        Debug.LogWarning("Sound: '" + name + "' not found. Cannot mute sound.");
    }

    public static void UnMute(string name)
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot unmute sound.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s == null)
                break;

            if (s.clips.Length == 0)
            {
                Debug.LogWarning(s.name + " has no clip(s) selected. Cannot unmute sound.");
                return;
            }

            if (s.name == name)
            {
                s.source.mute = false;
                return;
            }
        }

        Debug.LogWarning("Sound: '" + name + "' not found. Cannot unmute sound.");
    }

    public static void StopAllAudio()
    {
        if (instance.sounds.Count == 0)
        {
            Debug.LogWarning("Sounds array is empty. Cannot stop all audio.");
            return;
        }

        foreach (Sound s in instance.sounds)
        {
            if (s.clips.Length == 0)
                return;

            s.source.Stop();
        }
    }
}
