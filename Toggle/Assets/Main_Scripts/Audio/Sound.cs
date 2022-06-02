using UnityEngine;

[System.Serializable]
public class Sound
{
    #region fields

    [SerializeField]
    private string name;

    [SerializeField]
    private AudioClip clip;

    private AudioSource source;

    [Range(0f, 1f)]
    [SerializeField]
    private float volume;

    private float currentVolume;

    [Range(0.1f, 3f)]
    [SerializeField]
    private float pitch;

    [SerializeField]
    private bool loop;

    #endregion

    #region properties

    public string Name { get => name; set => name = value; }
    public AudioClip Clip { get => clip; set => clip = value; }
    public AudioSource Source { get => source; set => source = value; }
    public float Volume { get => volume; set => volume = value; }
    public float Pitch { get => pitch; set => pitch = value; }
    public bool Loop { get => loop; set => loop = value; }
    public float CurrentVolume { get => currentVolume; set => currentVolume = value; }

    #endregion
}
