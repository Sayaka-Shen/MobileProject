using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailGameMenu : MonoBehaviour
{
    [SerializeField] private Slider _sliderScore;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private float _speedSlider = 50f;
    private int _score = 0;
    [SerializeField] private Button _restartBt;
    [SerializeField] private Button _mainMenuBt;
    private bool _isSliderAnim = false;

    public void SetScore(int Score)
    {
        gameObject.SetActive(true);
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
