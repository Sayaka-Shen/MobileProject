using UnityEngine;
using UnityEngine.UI;

public class FailGameMenu : MonoBehaviour
{
    private GameObject _sliderScore;
    [SerializeField] private float _speedSlider = 1f;
    private int _score = 0;
    private GameObject _restartBt;
    private GameObject _mainMenuBt;
    private bool _isSliderAnim = false;
    [SerializeField] private GameObject _SceneManager;
    [SerializeField] private string _strSceneToLoad;
    [SerializeField] private DataLevelContainer _dataLevelContainer;

    void Start()
    {
        _sliderScore = this.transform.GetChild(1).gameObject;
        _restartBt = this.transform.GetChild(2).gameObject;
        _mainMenuBt = this.transform.GetChild(3).gameObject;
        _restartBt.GetComponent<Button>().onClick.AddListener(() =>
        {
            _SceneManager.GetComponent<SceneManager>().LoadScene(_strSceneToLoad);
        });
    }

    public void SetScore(int Score)
    {
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
