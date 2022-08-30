using System.Collections;
using UnityEngine;

public class AudioFadeEffect : MonoBehaviour
{
    private bool fadingIn, fadingOut;

    [HideInInspector] public Sound sound;

    private float reachedVolume;

    private float fadeOutTimer;
     
    private void Update()
    {
        if (fadingIn)
            sound.source.volume = sound.fadeInCurve.Evaluate(sound.source.time) * sound.volume;
        if (fadingOut)
        {
            fadeOutTimer += Time.deltaTime;
            sound.source.volume = sound.fadeOutCurve.Evaluate(fadeOutTimer % 60) * reachedVolume;
        }
    }

    public IEnumerator StartFadeIn()
    {
        fadingOut = false;
        fadingIn = true;
        sound.source.volume = 0.0f;
        yield return new WaitForSeconds(sound.fadeInCurve.length);
        if (fadingIn)
            fadingIn = false;
    }

    public IEnumerator StartFadeOut()
    {
        fadingIn = false;
        fadingOut = true;
        reachedVolume = sound.source.volume;
        fadeOutTimer = 0.0f;
        yield return new WaitForSeconds(sound.fadeOutCurve.length);
        if (fadingOut)
        {
            fadingOut = false;
            sound.source.volume = 0.0f;
            sound.source.Stop();
        }
    }
}
