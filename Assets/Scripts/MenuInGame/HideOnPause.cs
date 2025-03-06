using System.Collections.Generic;
using UnityEngine;

public class HideOnPause : MonoBehaviour
{
    [SerializeField]private List<GameObject> _gameObject = new List<GameObject>();


    void Update()
    {
        if (GameManager.Instance.Pause && _gameObject[0].activeSelf)
        {
            foreach(GameObject go in _gameObject)
            {
                go.SetActive(false);
            }
        }
        else if (!GameManager.Instance.Pause && !_gameObject[0].activeSelf)
        {
            foreach (GameObject go in _gameObject)
            {
                go.SetActive(true);
            }
        }
    }
}
