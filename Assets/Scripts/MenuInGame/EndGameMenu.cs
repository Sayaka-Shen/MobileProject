using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    private GameObject _stepText;
    private GameObject _timeText;
    private GameObject _sliderScore;
    private GameObject _nextLvBt;
    private GameObject _restartBt;
    private GameObject _mainMenuBt;
    [SerializeField] private float _speedSlider = 1f;
    private bool _isSliderAnim = false;
    private int _score = 0;
    [SerializeField] private GameObject _SceneManager;
    [SerializeField] private string _strSceneToLoad;
    [SerializeField] private DataLevelContainer _dataLevelContainer;
    void Start()
    {
        _stepText = this.transform.GetChild(2).GetChild(1).gameObject;
        _timeText = this.transform.GetChild(3).GetChild(1).gameObject;
        _sliderScore = this.transform.GetChild(1).gameObject;
        _restartBt = this.transform.GetChild(4).gameObject;
        _nextLvBt = this.transform.GetChild(5).gameObject;
        _mainMenuBt = this.transform.GetChild(6).gameObject;
        _restartBt.GetComponent<Button>().onClick.AddListener(() =>
        {
            _SceneManager.GetComponent<SceneManager>().LoadScene(_strSceneToLoad);
        });
        _nextLvBt.GetComponent<Button>().onClick.AddListener(() =>
        {
            _dataLevelContainer.NextLevel();
            _SceneManager.GetComponent<SceneManager>().LoadScene(_strSceneToLoad);
        });
        _mainMenuBt.GetComponent<Button>().onClick.AddListener(() =>
        {
            _SceneManager.GetComponent<SceneManager>().LoadScene("MenuScene");
        });
    }

    public void SetScore(int NbStep, float Time, int Score)
    {
        _stepText.GetComponent<TextMeshProUGUI>().text = NbStep.ToString();
        _timeText.GetComponent<TextMeshProUGUI>().text = Time.ToString();
        _score = Score;
        _isSliderAnim = true;
    }

    void Update()
    {
        if(_isSliderAnim)
        {
            if(_sliderScore.GetComponent<Slider>().value < _score)
            {
                _sliderScore.GetComponent<Slider>().value += _speedSlider * Time.deltaTime;
            }
            else
            {
                _isSliderAnim = false;
            }
        }
    }
}
