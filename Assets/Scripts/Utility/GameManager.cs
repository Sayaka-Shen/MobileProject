using System;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")] 
    private GameObject _player;
    public MovementPlayer MovementPlayer { get { return _player != null ? _player.GetComponent<MovementPlayer>() : throw new ArgumentNullException("No player movement"); } }
    public Vector3 PlayerPosition { get { return _player != null ? _player.transform.position : throw new ArgumentNullException("No player position"); } }
    public SoulPlayer SoulPlayer { get { return _player != null ? _player.gameObject.GetComponent<SoulPlayer>() : throw new ArgumentNullException("No soul player"); } }
    public Animator AnimPlayer { get { return _player != null ? _player.gameObject.GetComponent<MovementPlayer>().Animator : throw new ArgumentNullException("No anim player"); } }


    [Header("Level Settings")]
    private GameObject _level;
    [SerializeField] DataLevelContainer _levelContainer;

    [Header("Pathfinding Settings")]
    [SerializeField] private Grid _grid;

    [Header("End Settings")]
    [SerializeField] private GameObject _endUI;
    [SerializeField] private EndGameMenu _successUI;
    [SerializeField] private FailGameMenu _failedUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        loadLevel();
    }

    float _timer = 0f;
    public void StartTimer()
    {
        _timer = 0f;
    }

    void Update()
    {
        _timer += Time.deltaTime;
    }

    void Setup()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        SoulsManager.Instance.OnSoulChange += EndGame;
        StartTimer();
    }


    private void EndGame()
    {
        if (!SoulsManager.Instance.AllSoulsMeetEnd) return;
        int score = SoulsManager.Instance.CountSoulsPurify;
        int scorePercent = 0;
        float time = _timer;
        DataToSaves levelData = _levelContainer.GetCurrentLevel().DataToSaves;
        if (score != 0)
        {
            scorePercent = score * 100 / SoulsManager.Instance.CountSouls ;
            if (!levelData.IsCompleted || levelData.BestStep > MovementPlayer.NbCaseMouv) levelData.BestStep = MovementPlayer.NbCaseMouv;
            if (!levelData.IsCompleted || levelData.BestTime > time) levelData.BestTime = time;
            if (!levelData.IsCompleted || levelData.HighScore > SoulsManager.Instance.CountSoulsPurify) levelData.HighScore = SoulsManager.Instance.CountSoulsPurify;
            if (scorePercent >= 50)
            {
                levelData.IsCompleted = true;
                GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQAw");
                switch (_levelContainer.SceneToLoad)
                {
                    case (5):
                        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCg");
                        break;
                    case (10):
                        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCw");
                        break;
                    case (15):
                        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQDA");
                        break;
                    case (20):
                        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQDQ");
                        break;
                    case (25):
                        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQDg");
                        break;
                    case (30):
                        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQDw");
                        break;
                    default:
                        break;
                }
            }
        }
        SaveManager.Instance.Save(); 
        _endUI.SetActive(true);
        if (scorePercent >= 50) _successUI.SetScore(MovementPlayer.NbCaseMouv, time, scorePercent);
        else _failedUI.SetScore(scorePercent);
    }
    
    private void loadLevel()
    {
        if(_level != null) Destroy(_level);
        if(_levelContainer.SceneToLoad < 0 || _levelContainer.SceneToLoad >= _levelContainer.Levels.Length) throw new ArgumentNullException("No level selected");
        _level = Instantiate(_levelContainer.GetCurrentLevel().Prefab);
        SoulsManager.Instance.Setup();
        Setup();
        _grid.LoadGrid();
        _endUI.SetActive(false);
        _failedUI.gameObject.SetActive(false);
        _successUI.gameObject.SetActive(false);
        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQAQ");
    }

    public void restartLevel()
    {
        loadLevel();
    }
    public void nextLevel()
    {
        _levelContainer.NextLevel();
        loadLevel();
    }
}
