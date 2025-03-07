using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stepText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private Slider _sliderScore;
    [SerializeField] private Button _nextLvBt;
    [SerializeField] private Button _restartBt;
    [SerializeField] private Button _mainMenuBt;
    [SerializeField] private float _speedSlider = 50f;
    private bool _isSliderAnim = false;
    private int _score = 0;

    [SerializeField] DataLevelContainer _levelsContainer;
    
    public void SetScore(int NbStep, float Time, int Score)
    {
        gameObject.SetActive(true);
        _stepText.text = NbStep.ToString();
        _timeText.text = ((int)(Time / 60)).ToString() + "min" + ((int)(Time % 60)).ToString() + "s";
        _score = Score;
        _isSliderAnim = true;
        if (_levelsContainer.IsFinalLevel) _nextLvBt.gameObject.SetActive(false);
        else _nextLvBt.gameObject.SetActive(true);
        SfxManager.Instance.PlaySound2D("VictorySound");
    }

    void FixedUpdate()
    {
        if(_isSliderAnim)
        {
            if(_sliderScore.value < _score)
            {
                _sliderScore.value += _speedSlider * Time.fixedDeltaTime;
                _sliderText.text = ((int)_sliderScore.value).ToString() + "%";
            }
            else
            {
                _sliderScore.value = _score;
                _isSliderAnim = false;
            }
        }
    }
}
