using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip discoClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip killClip;
    [SerializeField] private AudioClip xpClip;
    [SerializeField] private AudioClip levelUpClip;

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        PlayBgmMusic();
    }

    public void PlayBgmMusic()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PlayDiscoMusic()
    {
        StartCoroutine(PlayDiscoThenBgm());
    }

    private IEnumerator PlayDiscoThenBgm()
    {
        bgmAudioSource.Stop();

        bgmAudioSource.clip = discoClip;
        bgmAudioSource.loop = false;
        bgmAudioSource.Play();

        yield return new WaitForSeconds(discoClip.length);

        PlayBgmMusic();
    }

    public void PlayHurtClip()
    {
        sfxAudioSource.PlayOneShot(hurtClip);
    }
    public void PlayKillClip()
    {
        sfxAudioSource.PlayOneShot(killClip);
    }

    public void PlayXPClip()
    {
        sfxAudioSource.PlayOneShot(xpClip);
    }

    public void PlayLevelUpClip()
    {
        sfxAudioSource.PlayOneShot(levelUpClip);
    }

}