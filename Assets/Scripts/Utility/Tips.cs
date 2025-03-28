using System;
using UnityEngine;

public class Tips : MonoBehaviour
{
    [SerializeField] private GameObject[] _text;
    private int _index = 0;
    public event Action TipsEnd;

    private void Awake()
    {
        foreach (GameObject item in _text)
        {
            item.SetActive(false);
        }
        _text[_index].SetActive(true);
    }

    public void Click()
    {
        _text[_index].SetActive(false);
        _index++;
        if(_index == _text.Length)
        {
            TipsEnd?.Invoke();
            _index = 0;
            foreach (GameObject item in _text)
            {
                item.SetActive(false);
            }
            _text[_index].SetActive(true);
        }
        else
        {
            _text[_index].SetActive(true);
        }
    }
}
