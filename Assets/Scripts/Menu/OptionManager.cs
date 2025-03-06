using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("Options UI")]
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private Slider _sliderVFX;
    [SerializeField] private Toggle _toggleMoveUI;

    public static event Action OptionChange;

    private void Start()
    {
        if (MusicManager.Instance != null)
        {
            _sliderMusic.onValueChanged.AddListener(UpdateMusic);
            _sliderMusic.value = PlayerPrefs.GetFloat("Music", 100);
            MusicManager.Instance.GetComponentInChildren<AudioSource>().volume = _sliderMusic.value;
        }
        if (AtmosphereManager.Instance != null)
        {
            _sliderSound.onValueChanged.AddListener(UpdateSound);
            _sliderSound.value = PlayerPrefs.GetFloat("Sound", 100);
            AtmosphereManager.Instance.GetComponentInChildren<AudioSource>().volume = _sliderSound.value;
        }
        if (SfxManager.Instance != null)
        {
            _sliderVFX.onValueChanged.AddListener(UpdateVFX);
            _sliderVFX.value = PlayerPrefs.GetFloat("VFX", 100);
            SfxManager.Instance.GetComponentInChildren<AudioSource>().volume = _sliderVFX.value;
        }
        _toggleMoveUI.onValueChanged.AddListener(UpdateMoveUI);
        _toggleMoveUI.isOn = (PlayerPrefs.GetInt("MoveUI", 1) == 1);
        MovementSprite.ShowAllButton = _toggleMoveUI.isOn;
        OptionChange?.Invoke();
    }

    private void UpdateMusic(float value)
    {
        MusicManager.Instance.GetComponentInChildren<AudioSource>().volume = value;
        PlayerPrefs.SetFloat("Music", value);
    }
    private void UpdateSound(float value)
    {
        AtmosphereManager.Instance.GetComponentInChildren<AudioSource>().volume = value;
        PlayerPrefs.SetFloat("Sound", value);
    }
    private void UpdateVFX(float value)
    {
        SfxManager.Instance.GetComponentInChildren<AudioSource>().volume = value;
        PlayerPrefs.SetFloat("VFX", value);
    }
    private void UpdateMoveUI(bool value)
    {
        MovementSprite.ShowAllButton = value;
        if (value) PlayerPrefs.SetInt("MoveUI", 1);
        else PlayerPrefs.SetInt("MoveUI", 0);
        OptionChange?.Invoke();
    }

}
