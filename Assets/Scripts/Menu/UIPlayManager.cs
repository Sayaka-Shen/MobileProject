using UnityEngine;

public class UIPlayManager : MonoBehaviour
{
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _play;
    [SerializeField] GameObject _option;


    public void Rollback()
    {
        RollbackManager.Instance.AddRollback();
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
    }
    public void Pause()
    {
        GameManager.Instance.Pause = true;
        Time.timeScale = 0f;
        _play.SetActive(false);
        _pause.SetActive(true);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
        MusicManager.Instance.StopMusic();
        MusicManager.Instance.ReplayMusic("MusicTitleScreen");
    }

    public void Play()
    {
        GameManager.Instance.Pause = false;
        Time.timeScale = 1.0f;
        _play.SetActive(true);
        _pause.SetActive(false);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
        MusicManager.Instance.ReplayMusic("MusicInGame");
    }
    public void Option()
    {
        _option.SetActive(true);
        _pause.SetActive(false);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
    }
    public void Return()
    {
        _option.SetActive(false);
        _pause.SetActive(true);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
    }
}
