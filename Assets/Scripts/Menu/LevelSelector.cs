using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private DataLevelContainer _dataLevelContainer;
    [SerializeField] private GameObject _levelButtonPrefab;
    [SerializeField] private GameObject _levelStarter;
    [SerializeField] private GameObject SceneManager;
    [SerializeField] private string _strSceneToLoad;

    void Start()
    {
        List<DataLevel> levels = new List<DataLevel>(_dataLevelContainer.Levels);
        for (int i = 0; i< levels.Count; i++)
        {
            GameObject levelButton = Instantiate(_levelButtonPrefab, transform);
            levelButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = levels[i].ImagePreviewMini;
            levelButton.transform.GetChild(2).gameObject.SetActive(true);
            levelButton.transform.GetChild(2).GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = levels[i].DataToSaves.PercentFinish.ToString() + "%";
            levelButton.transform.GetChild(2).GetComponent<Slider>().value = levels[i].DataToSaves.PercentFinish;
            levelButton.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = i+1.ToString();
            if(i>0 && !levels[i-1].DataToSaves.IsCompleted)
            {
                levelButton.transform.GetChild(1).gameObject.SetActive(true);
                levelButton.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        float fixposition = (int)(levels.Count / 4) * 600;
        this.transform.position += new Vector3(fixposition, 0, 0);
    }

    private void Change(int i, List<DataLevel> levels)
    {
        _levelStarter.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = levels[i].ImagePreview;
        _levelStarter.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = levels[i].LevelName;
        _levelStarter.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = levels[i].DataToSaves.HighScore.ToString();
        _levelStarter.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = levels[i].DataToSaves.BestStep.ToString();
        _levelStarter.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = levels[i].DataToSaves.BestTime.ToString();
        _levelStarter.transform.GetChild(6).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            _dataLevelContainer.SceneToLoad = i;
            SceneManager.GetComponent<SceneManager>().LoadScene(_strSceneToLoad);
        });
        _levelStarter.SetActive(true);
        _levelStarter.transform.GetChild(7).GetComponent<Slider>().value = levels[i].DataToSaves.PercentFinish;
        _levelStarter.transform.GetChild(7).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = levels[i].DataToSaves.PercentFinish.ToString() + "%";
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }
}
