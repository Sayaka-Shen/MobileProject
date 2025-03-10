using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelector;
    [SerializeField] private GameObject _startcontainer;
    [SerializeField] private GameObject _optionContainer;
    [SerializeField] private GameObject _TransitionContainer;
    [SerializeField] private GameObject _LogoScreen;
    [SerializeField] private DataLevelContainer _dataLevelContainer;
    private float time = 0;

    public void Start()
    {
        MusicManager.Instance.PlayMusic("MusicTitleScreen");
    }

    public void ReturnSelector()
    {
        _levelSelector.SetActive(true);
        _optionContainer.SetActive(false);
        _TransitionContainer.SetActive(false);
        _startcontainer.SetActive(false);
    }
    public void StartTransition()
    {
        _TransitionContainer.SetActive(true);
        _LogoScreen.SetActive(false);
    }

    public void ReturnMenuBt()
    {
        _levelSelector.SetActive(false);
        _startcontainer.SetActive(true);
        _optionContainer.SetActive(false);
        _TransitionContainer.SetActive(false);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
    }

    public void OptionMenu()
    {
        _optionContainer.SetActive(true);
        _levelSelector.SetActive(false);
        _startcontainer.SetActive(false);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
    }

    void Awake()
    {
        _levelSelector.SetActive(true);
        time = Time.realtimeSinceStartup;
        if(time<30)
        {
            _levelSelector.SetActive(false);
            _LogoScreen.SetActive(true);
            _LogoScreen.GetComponent<Animation>().Play();
            _dataLevelContainer.SceneToLoad = 0;
        }
    }
}
