using UnityEngine;

public class UIPlayManager : MonoBehaviour
{
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _play;
    [SerializeField] GameObject _option;


    public void Rollback()
    {
        RollbackManager.Instance.AddRollback();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        _play.SetActive(false);
        _pause.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
        _play.SetActive(true);
        _pause.SetActive(false);
    }
    public void Option()
    {
        _option.SetActive(true);
        _pause.SetActive(false);
    }
    public void Return()
    {
        _option.SetActive(false);
        _pause.SetActive(true);
    }
}
