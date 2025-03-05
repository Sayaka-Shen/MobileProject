using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    
    [Header("Music Manager Settings")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private MusicLibrary _musicLibrary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string musicName, float fadeDuration = 0.1f)
    {
        StartCoroutine(AnimateMusicCrossFade(_musicLibrary.GetMusicFromName(musicName), fadeDuration));
    }

    IEnumerator AnimateMusicCrossFade(AudioClip nextMusic, float fadeDuration = 0.1f)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            _musicSource.volume = Mathf.Lerp(_musicSource.volume, 0, percent);
            yield return null;
        }
        
        _musicSource.clip = nextMusic;
        _musicSource.Play();

        percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            _musicSource.volume = Mathf.Lerp(0, _musicSource.volume, percent);
            yield return null;
        }
    }

}
