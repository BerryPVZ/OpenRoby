using System;
using UnityEngine;

public static class AudioRuntime
{
    private const float DefaultSfxVolume = 1f;

    public static AudioClip LoadClip(string resourcePath)
    {
        if (string.IsNullOrEmpty(resourcePath))
        {
            return null;
        }

        AudioClip clip = Resources.Load<AudioClip>(resourcePath);
        if (clip != null)
        {
            return clip;
        }

        string alternatePath = resourcePath;
        if (resourcePath.StartsWith("sounds/", StringComparison.Ordinal))
        {
            alternatePath = "Sounds/" + resourcePath.Substring(7);
        }
        else if (resourcePath.StartsWith("Sounds/", StringComparison.Ordinal))
        {
            alternatePath = "sounds/" + resourcePath.Substring(7);
        }

        if (alternatePath != resourcePath)
        {
            clip = Resources.Load<AudioClip>(alternatePath);
        }

        if (clip == null)
        {
            Debug.LogWarning("Audio clip was not found in Resources: " + resourcePath);
        }
        return clip;
    }

    public static AudioSource Ensure2DSource(GameObject host, float volume = DefaultSfxVolume)
    {
        if (host == null)
        {
            return null;
        }

        AudioSource source = host.GetComponent<AudioSource>();
        if (source == null)
        {
            source = host.AddComponent<AudioSource>();
        }

        source.playOnAwake = false;
        source.spatialBlend = 0f;
        source.volume = Mathf.Clamp01(volume);
        return source;
    }

    public static bool Play(AudioSource source, AudioClip clip, float volume = DefaultSfxVolume, bool restart = true)
    {
        if (!PlayerPreperencesManager.GetSound() || source == null || clip == null)
        {
            return false;
        }

        source.playOnAwake = false;
        source.spatialBlend = 0f;
        source.volume = Mathf.Clamp01(volume);
        if (restart && source.isPlaying)
        {
            source.Stop();
        }
        source.clip = clip;
        source.Play();
        return true;
    }

    public static bool PlayOneShot(AudioClip clip, float volume = DefaultSfxVolume)
    {
        if (!PlayerPreperencesManager.GetSound() || clip == null)
        {
            return false;
        }

        GameObject host = new GameObject("AudioRuntime_OneShot_" + clip.name);
        AudioSource source = host.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0f;
        source.volume = Mathf.Clamp01(volume);
        source.clip = clip;
        source.Play();

        float lifetime = clip.length > 0f ? clip.length + 0.25f : 10f;
        UnityEngine.Object.Destroy(host, lifetime);
        return true;
    }
}
