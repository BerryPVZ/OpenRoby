using System.Collections;
using UnityEngine;

public class Music_comics : MonoBehaviour
{
    public int p;
    public AudioClip[] sounds;

    private AudioSource musicSource;

    private void Start()
    {
        LoadSounds();
        musicSource = AudioRuntime.Ensure2DSource(gameObject, 1f);
        if (musicSource != null)
        {
            musicSource.loop = false;
        }
        Play_sound(0);
    }

    private void LoadSounds()
    {
        switch (p)
        {
        case 1:
            sounds = new AudioClip[27];
            sounds[0] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_comics_City");
            sounds[1] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_eye_1");
            sounds[2] = AudioRuntime.LoadClip("Sounds/Comics1/Robo jump");
            sounds[3] = AudioRuntime.LoadClip("Sounds/Comics1/Robo lump");
            sounds[4] = AudioRuntime.LoadClip("Sounds/Comics1/Robo thought 2");
            sounds[5] = AudioRuntime.LoadClip("Sounds/Comics1/Robo wake up");
            sounds[6] = AudioRuntime.LoadClip("Sounds/Comics1/bable");
            sounds[7] = AudioRuntime.LoadClip("Sounds/Comics1/matrix");
            sounds[8] = AudioRuntime.LoadClip("Sounds/Comics1/Robo lifting");
            sounds[9] = AudioRuntime.LoadClip("Sounds/Comics1/Robo look");
            sounds[10] = AudioRuntime.LoadClip("Sounds/Comics1/Robo to the box");
            sounds[11] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_Alarm");
            sounds[12] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_Approved");
            sounds[13] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_comics_City");
            sounds[14] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_determinition");
            sounds[15] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_emotions");
            sounds[16] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_frighten");
            sounds[17] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_looks_on_frame");
            sounds[18] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_Rejected");
            sounds[19] = AudioRuntime.LoadClip("Sounds/Comics1/Robo_smile");
            sounds[20] = AudioRuntime.LoadClip("Sounds/Comics1/Shows_tongue");
            sounds[21] = AudioRuntime.LoadClip("Sounds/Comics1/tolkach 2");
            sounds[22] = AudioRuntime.LoadClip("sounds/Lift");
            sounds[23] = AudioRuntime.LoadClip("Sounds/Comics1/scan");
            sounds[24] = AudioRuntime.LoadClip("Sounds/Comics1/fight");
            sounds[25] = AudioRuntime.LoadClip("Sounds/Comics1/fight(laugh)");
            sounds[26] = AudioRuntime.LoadClip("Sounds/Comics1/robo_pushed");
            break;
        case 2:
            sounds = new AudioClip[9];
            sounds[0] = AudioRuntime.LoadClip("Sounds/Comics2/RoboComics 2");
            sounds[1] = AudioRuntime.LoadClip("Sounds/Comics2/Hello");
            sounds[2] = AudioRuntime.LoadClip("Sounds/Comics2/wtf");
            sounds[3] = AudioRuntime.LoadClip("Sounds/Comics2/wow");
            sounds[4] = AudioRuntime.LoadClip("Sounds/Comics2/Metallic Hit");
            sounds[5] = AudioRuntime.LoadClip("Sounds/Comics2/geronimo");
            sounds[6] = AudioRuntime.LoadClip("Sounds/Comics2/Robo jump");
            sounds[7] = AudioRuntime.LoadClip("Sounds/Comics2/jump");
            sounds[8] = AudioRuntime.LoadClip("Sounds/Comics2/win soldier");
            break;
        case 3:
            sounds = new AudioClip[7];
            sounds[0] = AudioRuntime.LoadClip("Sounds/Comics3/Comics 3");
            sounds[1] = AudioRuntime.LoadClip("Sounds/Comics3/expl box 3");
            sounds[2] = AudioRuntime.LoadClip("Sounds/Comics3/girl jump 2");
            sounds[3] = AudioRuntime.LoadClip("Sounds/Comics3/jumping box");
            sounds[4] = AudioRuntime.LoadClip("Sounds/Comics3/kiss 4");
            sounds[5] = AudioRuntime.LoadClip("Sounds/Comics3/look down");
            sounds[6] = AudioRuntime.LoadClip("Sounds/Comics1/Robo jump");
            break;
        case 4:
            sounds = new AudioClip[4];
            sounds[0] = AudioRuntime.LoadClip("Sounds/Comics4/Comics 4");
            sounds[1] = AudioRuntime.LoadClip("Sounds/Comics4/tech robo lift");
            sounds[2] = AudioRuntime.LoadClip("Sounds/Comics4/twaddle");
            sounds[3] = AudioRuntime.LoadClip("Sounds/Comics4/WOW");
            break;
        default:
            sounds = new AudioClip[0];
            break;
        }
    }

    private void Play_base()
    {
        Play_sound(0);
    }

    private IEnumerator Change_volume(int increase)
    {
        if (musicSource == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.125f);
        if (increase == 1)
        {
            for (float i = musicSource.volume; i <= 1f; i += 0.1f)
            {
                yield return new WaitForSeconds(0.125f);
                musicSource.volume = i;
            }
        }
        else
        {
            for (float i = musicSource.volume; i >= 0f; i -= 0.1f)
            {
                yield return new WaitForSeconds(0.125f);
                musicSource.volume = i;
            }
        }
    }

    public void Play_sound(int n)
    {
        if (sounds == null || n < 0 || n >= sounds.Length || sounds[n] == null)
        {
            Debug.LogWarning("Comic audio index is invalid or missing: " + n);
            return;
        }

        if (n == 0)
        {
            if (!PlayerPreperencesManager.GetMusic() || musicSource == null)
            {
                return;
            }

            musicSource.spatialBlend = 0f;
            musicSource.clip = sounds[0];
            musicSource.Play();
            return;
        }

        AudioRuntime.PlayOneShot(sounds[n]);
    }

    public void New_s(int n)
    {
        Play_sound(n);
    }
}
