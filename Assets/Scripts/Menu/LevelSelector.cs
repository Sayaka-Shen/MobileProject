using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private DataLevelContainer _dataLevelContainer;
    [SerializeField] private GameObject _levelButtonPrefab;
    [SerializeField] private GameObject _levelStarter;
    [SerializeField] private SceneManager SceneManager;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _stepText;
    [SerializeField] private UnityEngine.UI.Button _playButton;
    [SerializeField] private UnityEngine.UI.Image _playBtImage;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _lockedSprite;
    private RectTransform _rectTransform;
    bool _isLevelEven = false;
    private int _levelsCount;
    private float _levelWidth;
    private bool _isEndDragging = false;
    private float nextPos = 0;
    private bool _isLevelSelected = false;
    private Vector2 _tempPos;
    private int _levelSelected = 0;
    private List<DataLevel> _levels;
    private List<GameObject> _levelButtons = new List<GameObject>();

    void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _levels = new List<DataLevel>(_dataLevelContainer.Levels);
        _levelWidth = this.GetComponent<GridLayoutGroup>().cellSize.x + this.GetComponent<GridLayoutGroup>().spacing.x;
        _levelsCount = _levels.Count;
        for (int i = 0; i< _levelsCount; i++)
        {
            GameObject levelButton = Instantiate(_levelButtonPrefab, transform);
            _levelButtons.Add(levelButton);
            levelButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = _levels[i].ImagePreviewMini;
            levelButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Beta");
            });
            levelButton.transform.GetChild(2).gameObject.SetActive(true);
            levelButton.transform.GetChild(2).GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = _levels[i].DataToSaves.PercentFinish.ToString() + "%";
            levelButton.transform.GetChild(2).GetComponent<UnityEngine.UI.Slider>().value = _levels[i].DataToSaves.PercentFinish;
            levelButton.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            if(i>0 && !_levels[i-1].DataToSaves.IsCompleted)
            {
                levelButton.transform.GetChild(1).gameObject.SetActive(true);
                levelButton.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        GameObject EmptyGO = Instantiate(new GameObject(),transform);
        EmptyGO.AddComponent<RectTransform>();
        if (_levelsCount % 2 == 0)
        {
            _isLevelEven = true;
            this.transform.position += new Vector3(_levelWidth*(int)((_levelsCount / 2) -1)+(_levelWidth/2)-92, 0, 0);
        }
        else
        {
            _isLevelEven = false;
            this.transform.position += new Vector3(_levelWidth*(int)(_levelsCount / 2)-92, 0, 0);
        }
        this.GetComponent<BoxCollider2D>().size = new Vector2((_levelsCount+2)*_levelWidth, this.GetComponent<RectTransform>().sizeDelta.y);
        int time = (int)_levels[_levelSelected].DataToSaves.BestTime;
        _timeText.text = (time / 60).ToString() + "min " + (time % 60).ToString() + "s";
        _stepText.text = _levels[0].DataToSaves.BestStep.ToString();
        _dataLevelContainer.SceneToLoad = 0;
        _playButton.interactable = true;
        _playBtImage.sprite = _playSprite;
    }

    void OnMouseUp()
    {
        _isEndDragging = true;
    }

    void CenterPrefab()
    {
        _scrollRect.velocity = Vector2.zero;
        int tempX = (int)_rectTransform.anchoredPosition.x;
        switch(_isLevelEven)
        {
            case true:
            nextPos = (_levelWidth * Mathf.Round((tempX-600) / _levelWidth))+600;
            if(nextPos > _levelWidth*(int)(_levelsCount / 2)+(_levelWidth/2))
            {
                nextPos = _levelWidth*(int)(_levelsCount / 2)+(_levelWidth/2);
            }
            else if(nextPos < -_levelWidth*(int)((_levelsCount / 2) -1)+(_levelWidth/2))
            {
                nextPos = -_levelWidth*(int)((_levelsCount / 2) -1)+(_levelWidth/2);
            }
            _levelSelected = (int)-((nextPos-600)/_levelWidth) + (_levelsCount / 2);
            break;
            case false:
            nextPos = _levelWidth * Mathf.Round(tempX / _levelWidth);
            if(nextPos > _levelWidth*(int)(_levelsCount / 2)+_levelWidth)
            {
                nextPos = _levelWidth*(int)(_levelsCount / 2)+_levelWidth;
            }
            else if(nextPos < -_levelWidth*(int)(_levelsCount / 2)+_levelWidth)
            {
                nextPos = -_levelWidth*(int)(_levelsCount / 2)+_levelWidth;
            }
            _levelSelected = (int)-(nextPos/_levelWidth) + (_levelsCount / 2)+1;
            break;
        }
        _tempPos = new Vector2(nextPos, -873);
        int time = (int)_levels[_levelSelected].DataToSaves.BestTime;
        _timeText.text = (time / 60).ToString() + "min " + (time % 60).ToString() + "s";
        _stepText.text = _levels[_levelSelected].DataToSaves.BestStep.ToString();
        _levelButtons[_levelSelected].transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().enabled = true;
        if(_levelSelected>0 &&_levels[_levelSelected-1].DataToSaves.IsCompleted)
        {
            _playButton.interactable = true;
            _playBtImage.sprite = _playSprite;
            _levelButtons[_levelSelected-1].transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
        else if(_levelSelected == 0)
        {
            _playButton.interactable = true;
            _playBtImage.sprite = _playSprite;
        }
        else
        {
            _levelButtons[_levelSelected-1].transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().enabled = false;
            Debug.Log(_levelSelected);
            _playButton.interactable = false;
            _playBtImage.sprite = _lockedSprite;
        }
        if(_levelSelected != _levelsCount)
        {
            _levelButtons[_levelSelected+1].transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
        _dataLevelContainer.SceneToLoad = _levelSelected;
        _isLevelSelected = true;
    }

    void Update()
    {
        if(_isEndDragging && _scrollRect.velocity.x <= 500 && _scrollRect.velocity.x >= -500)
        {
            CenterPrefab();
            _isEndDragging = false;
        }
        if(_isLevelSelected)
        {
            _scrollRect.velocity = Vector2.zero;
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _tempPos, 0.05f);
            if(Vector2.Distance(_rectTransform.anchoredPosition, _tempPos) < 10f)
            {
                _rectTransform.anchoredPosition = _tempPos;
                _isLevelSelected = false;
            }
        }
    }
}
