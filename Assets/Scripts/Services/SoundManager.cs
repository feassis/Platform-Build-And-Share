using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource;

    [Header("SFX Pool")]
    [SerializeField] private AudioSource[] sfxSources;

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float bgmVolume = 0.5f;

    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 0.5f;

    private int currentSFXIndex = 0;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ApplyVolumes();
    }

    // ==================================================
    // BGM
    // ==================================================

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    // ==================================================
    // SFX
    // ==================================================

    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = GetAvailableSFXSource();

        source.spatialBlend = 0f;

        source.clip = clip;
        source.volume = sfxVolume;
        source.Play();
    }

    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        AudioSource source = GetAvailableSFXSource();

        source.transform.position = position;

        source.spatialBlend = 1f;

        source.clip = clip;
        source.volume = sfxVolume;
        source.Play();
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        AudioSource fallback = sfxSources[currentSFXIndex];

        currentSFXIndex++;

        if (currentSFXIndex >= sfxSources.Length)
            currentSFXIndex = 0;

        return fallback;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);

        foreach (AudioSource source in sfxSources)
        {
            source.volume = sfxVolume;
        }
    }

    // ==================================================
    // AUX
    // ==================================================

    private void ApplyVolumes()
    {
        bgmSource.volume = bgmVolume;

        foreach (AudioSource source in sfxSources)
        {
            source.volume = sfxVolume;
        }
    }
}