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
    
    public void SetScore(int NbStep, float Time, int Score)
    {
        gameObject.SetActive(true);
        _stepText.text = NbStep.ToString();
        _timeText.text = ((int)(Time / 60)).ToString() + "min" + ((int)(Time % 60)).ToString() + "s";
        _score = Score;
        _isSliderAnim = true;
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
