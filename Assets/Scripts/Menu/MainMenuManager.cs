using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelector;
    [SerializeField] private GameObject _startcontainer;
    [SerializeField] private GameObject _optionContainer;
    [SerializeField] private GameObject _TransitionContainer;
    private float time = 0;


    public void ReturnSelector()
    {
        _levelSelector.SetActive(true);
        _optionContainer.SetActive(false);
        _TransitionContainer.SetActive(false);
        SfxManager.Instance.PlaySound2D("ClickMenuSound");
    }
    public void StartTransition()
    {
        _TransitionContainer.SetActive(true);
        _startcontainer.SetActive(false);
    }

    public void ReturnMenuBt()
    {
        _levelSelector.SetActive(false);
        _startcontainer.SetActive(true);
        _optionContainer.SetActive(false);
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
            _startcontainer.SetActive(true);
        }
    }
}
