using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    //public AudioMixerGroup uiMixer, sfxMixer;
    public AudioClip[] clips;
    public Queue<AudioSource> monoSources;
    public Queue<AudioSource> stereoSources;

    private void Awake()
    {
        CreateAudioPools();
    }

    public void CreateAudioPools()
    {
        monoSources = new Queue<AudioSource>();
        for (int i = 0; i < 20; i++)
        {
            GameObject newSource = new GameObject();
            newSource.transform.parent = transform;
            newSource.name = "Source " + i;
            AudioSource audioSource = newSource.AddComponent<AudioSource>();
            //audioSource.outputAudioMixerGroup = uiMixer;
            audioSource.spatialBlend = 0;
            audioSource.volume = 0.2f;
            monoSources.Enqueue(audioSource);
        }

        stereoSources = new Queue<AudioSource>();
        for (int i = 0; i < 20; i++)
        {
            GameObject newSource = new GameObject();
            newSource.transform.parent = transform;
            newSource.name = "Stereo_Source " + i;
            AudioSource audioSource = newSource.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1;
            audioSource.volume = 1f;
            audioSource.maxDistance = 20;
            stereoSources.Enqueue(audioSource);
        }
    }

    public void PlayMonoSound(int no) //2D
    {
        AudioSource audioSource = GetMonoSource();
        audioSource.clip = clips[no];
        audioSource.Play();
    }
    public AudioSource GetMonoSource()
    {
        AudioSource s = monoSources.Dequeue();
        monoSources.Enqueue(s);

        return s;
    }
    public void PlayStereoSound(int no, Vector3 position) //3D
    {
        AudioSource audioSource = GetStereoSource();
        audioSource.transform.position = position;
        audioSource.clip = clips[no];
        audioSource.Play();
    }
    public AudioSource GetStereoSource()
    {
        AudioSource s = stereoSources.Dequeue();
        stereoSources.Enqueue(s);

        return s;
    }
}