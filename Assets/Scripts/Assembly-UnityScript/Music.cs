using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Music : MonoBehaviour
{
    public double vol = 0.4000000059604645;
    public AudioClip sound_menu;
    public AudioClip sound_game;

    private AudioSource source;

    public virtual void Start()
    {
        source = AudioRuntime.Ensure2DSource(gameObject, (float)vol);
        if (source != null)
        {
            source.loop = true;
        }
        StartCoroutine(update_music());
    }

    public virtual IEnumerator update_music()
    {
        while (true)
        {
            UpdateMusicState();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void UpdateMusicState()
    {
        if (source == null)
        {
            source = AudioRuntime.Ensure2DSource(gameObject, (float)vol);
            if (source == null)
            {
                return;
            }
            source.loop = true;
        }

        bool musicEnabled = PlayerPreperencesManager.GetMusic();
        bool comicsVisible = GameObject.FindGameObjectWithTag("comics") != null;
        bool menuVisible = GameObject.FindGameObjectWithTag("menu") != null;
        AudioClip wantedClip = menuVisible ? sound_menu : sound_game;

        if (wantedClip != null && source.clip != wantedClip)
        {
            source.Stop();
            source.clip = wantedClip;
        }

        source.volume = musicEnabled && !comicsVisible
            ? (menuVisible ? 1f : Mathf.Clamp01((float)vol))
            : 0f;

        if (musicEnabled && !comicsVisible && wantedClip != null && !source.isPlaying)
        {
            source.Play();
        }
    }

    public virtual void Main()
    {
    }
}
