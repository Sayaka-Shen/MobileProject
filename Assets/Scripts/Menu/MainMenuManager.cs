using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelector;
    [SerializeField] private GameObject _startcontainer;
    [SerializeField] private GameObject _optionContainer;
    private float time = 0;


    public void ReturnSelector()
    {
        _levelSelector.SetActive(true);
        _startcontainer.SetActive(false);
        _optionContainer.SetActive(false);
    }

    public void ReturnMenuBt()
    {
        _levelSelector.SetActive(false);
        _startcontainer.SetActive(true);
        _optionContainer.SetActive(false);
        //_introContainer.SetActive(false);
    }

    public void OptionMenu()
    {
        _optionContainer.SetActive(true);
        _levelSelector.SetActive(false);
        _startcontainer.SetActive(false);
    }

    void Awake()
    {
        time = Time.realtimeSinceStartup;
        if(time<10)
        {
            _optionContainer.SetActive(false);
            _levelSelector.SetActive(false);
            _startcontainer.SetActive(true);
        }
        else
        {
            _optionContainer.SetActive(false);
            _levelSelector.SetActive(true);
            _startcontainer.SetActive(false);
        }
    }
}
