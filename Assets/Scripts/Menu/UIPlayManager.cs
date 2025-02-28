using UnityEngine;

public class UIPlayManager : MonoBehaviour
{
    [SerializeField] GameObject _Pause;
    [SerializeField] GameObject _Play;
    [SerializeField] GameObject _Option;

    public void Pause()
    {
        Time.timeScale = 0f;
        _Play.SetActive(false);
        _Pause.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
        _Play.SetActive(true);
        _Pause.SetActive(false);
    }
    public void Option()
    {
        _Option.SetActive(true);
        _Pause.SetActive(false);
    }
    public void Return()
    {
        _Option.SetActive(false);
        _Pause.SetActive(true);
    }
}
