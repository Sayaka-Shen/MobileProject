using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelector;
    [SerializeField] private GameObject _btContainer;
    [SerializeField] private GameObject _optionContainer;
    //[SerializeField] private GameObject _introContainer;


    public void ReturnSelector()
    {
        _levelSelector.SetActive(true);
        _btContainer.SetActive(false);
        _optionContainer.SetActive(false);
    }

    public void ReturnMenuBt()
    {
        _levelSelector.SetActive(false);
        _btContainer.SetActive(true);
        _optionContainer.SetActive(false);
        //_introContainer.SetActive(false);
    }

    public void OptionMenu()
    {
        _optionContainer.SetActive(true);
        _levelSelector.SetActive(false);
        _btContainer.SetActive(false);
    }
}
