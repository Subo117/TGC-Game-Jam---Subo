using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip discoClip;
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

    public void PlayDiscoMusic()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = discoClip;
        bgmAudioSource.Play();
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