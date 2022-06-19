using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region fields

    public static AudioManager instance;

    [Header("Sounds")]

    [SerializeField]
    private List<Sound> soundFX;
    [SerializeField]
    private Sound music;

    [Header("Volume")]

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float soundFXVolume;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float musicVolume;

    #endregion

    #region monobehaviour

    void Awake()
    {
        GetInstance();

        SetupSounds();
    }

    void Start()
    {
        // play the music if it exists
        if (music == null)
        {
            Debug.LogWarning("Music was not found.");
        }
        else
        {
            PlaySound(music);
        }
    }

    #endregion

    #region initialization

    /// <summary>
    /// A singleton used to ensure there is only one AudioManager instance.
    /// </summary>
    private void GetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Loops through each Sound found in the soundFX variable to initialize them and set their volumes.
    /// Does the same for the music.
    /// </summary>
    private void SetupSounds()
    {
        // setup soundFX
        foreach (Sound sound in soundFX)
        {
            InitializeSound(sound);
            SetVolume(sound, soundFXVolume);
        }
        // setup music
        InitializeSound(music);
        SetVolume(music, musicVolume);
    }

    /// <summary>
    /// Sets the values for the AudioSource of the provided Sound instance.
    /// </summary>
    /// <param name="sound">Contains AudioSource and the values to have the AudioSource set too.</param>
    public void InitializeSound(Sound sound)
    {
        sound.Source = gameObject.AddComponent<AudioSource>();
        sound.Source.clip = sound.Clip;
        sound.Source.volume = sound.Volume;
        sound.Source.pitch = sound.Pitch;
        sound.Source.loop = sound.Loop;
    }

    #endregion

    #region sound control

    /// <summary>
    /// Finds the Sound in the soundFX collection with the specified name and plays it if found.
    /// </summary>
    /// <param name="name">The name of the Sound to find inside the soundFX collection.</param>
    public void PlaySFX (string name)
    {
        Sound sFX = soundFX.Find(sound => sound.Name.Equals(name));

        if (sFX == null)
        {
            Debug.LogWarning("Sound " + name + " was not found.");
        }
        else
        {
            PlaySound(sFX);
        }
    }

    /// <summary>
    /// Changes the volume of the AudioSource in the specified Sound parameter.
    /// </summary>
    /// <param name="sound">The sound to have its AudioSource changed.</param>
    /// <param name="percentage">The value used to adjust the volume inside the AudioSource of the specified Sound.</param>
    public void SetVolume(Sound sound, float percentage)
    {
        sound.CurrentVolume = percentage * sound.Volume;
        sound.Source.volume = sound.CurrentVolume;
    }

    public void SetFXVolume(float percentage)
    {
        foreach (Sound sound in soundFX)
        {
            SetVolume(sound, percentage);
        }
    }

    public void SetMusicVolume(float percentage)
    {
        SetVolume(music, percentage);
    }

    /// <summary>
    /// Plays the specified Sound parameter if not null.
    /// </summary>
    /// <param name="sound">The sound to have its AudioSource played.</param>
    private void PlaySound(Sound sound)
    {
        sound?.Source.Play();
    }

    #endregion
}
