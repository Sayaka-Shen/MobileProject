using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayManager : MonoBehaviour
{
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _play;
    [SerializeField] GameObject _option;
    [SerializeField] GameObject _TipsButton;
    [SerializeField] Button[] _TipsButtonsToStop;
    private GameObject _TipsUI;

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

    public void Tips()
    {
        _TipsUI.SetActive(true);
        foreach (Button item in _TipsButtonsToStop)
        {
            item.interactable = false;
        }
        _TipsUI.GetComponent<Tips>().TipsEnd += ShowButtons;
    }

    private void ShowButtons()
    {
        _TipsUI.SetActive(false);
        foreach (Button item in _TipsButtonsToStop)
        {
            item.interactable = true;
        }
    }

    public void ShowTips()
    {
        _TipsUI = GameObject.FindGameObjectWithTag("Tips");
        if (_TipsUI == null)
        {
            _TipsButton.SetActive(false);
        }
        else
        {
            _TipsButton.SetActive(true);
            Tips();
        }
    }
}
