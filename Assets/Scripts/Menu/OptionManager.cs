using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class OptionManager : MonoBehaviour
{
    [Header("Options UI")]
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private Slider _sliderVFX;
    [SerializeField] private Toggle _toggleMoveUI;

    private void Start()
    {
        if (MusicManager.Instance != null)
        {
            _sliderMusic.onValueChanged.AddListener(UpdateMusic);
            _sliderMusic.value = PlayerPrefs.GetFloat("Music");
            MusicManager.Instance.GetComponentInChildren<AudioSource>().volume = _sliderMusic.value;
        }
        if (AtmosphereManager.Instance != null)
        {
            _sliderSound.onValueChanged.AddListener(UpdateSound);
            _sliderSound.value = PlayerPrefs.GetFloat("Sound");
            AtmosphereManager.Instance.GetComponentInChildren<AudioSource>().volume = _sliderSound.value;
        }
        if (SfxManager.Instance != null)
        {
            _sliderVFX.onValueChanged.AddListener(UpdateVFX);
            _sliderVFX.value = PlayerPrefs.GetFloat("VFX");
            SfxManager.Instance.GetComponentInChildren<AudioSource>().volume = _sliderVFX.value;
        }
        _toggleMoveUI.onValueChanged.AddListener(UpdateMoveUI);
        _toggleMoveUI.isOn = (PlayerPrefs.GetInt("MoveUI") == 1);
        MovementButton.ShowAllButton = _toggleMoveUI.isOn;
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
        MovementButton.ShowAllButton = value;
        if (value) PlayerPrefs.SetInt("MoveUI", 1);
        else PlayerPrefs.SetInt("MoveUI", 0);
    }

}
