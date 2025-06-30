using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Setup")]
    public AudioSource musicSource;
    public AudioMixer audioMixer;

    [Header("Clips")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private AudioClip currentClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (musicSource == null)
                musicSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        // Escenas de menú, tienda, opciones, etc.
        if (sceneName.Contains("Menu") || sceneName.Contains("Tienda") || sceneName.Contains("Opciones"))
        {
            PlayMenuMusic();
        }
        // Escenas de juego
        else if (sceneName.StartsWith("LevelOne"))
        {
            PlayGameMusic();
        }
    }

    public void PlayMenuMusic()
    {
        if (currentClip == menuMusic) return;

        musicSource.clip = menuMusic;
        musicSource.loop = true;
        currentClip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        if (currentClip == gameMusic) return;

        musicSource.clip = gameMusic;
        musicSource.loop = true;
        currentClip = gameMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        currentClip = null;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetAmbientVolume(float volume)
    {
        audioMixer.SetFloat("AmbientVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }
}
