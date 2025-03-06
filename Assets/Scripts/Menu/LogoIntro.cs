using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LogoIntro : MonoBehaviour
{
    [SerializeField] private MainMenuManager _mainMenuManager;
    [SerializeField] private float _timeToWait = 30f;
    [SerializeField] private Animation _animation;
    private bool _isWaiting = false;

    void Update()
    {
        if(_animation.isPlaying)
        {
            return;
        }
        else if (!_isWaiting)
        {
            StartCoroutine(Wait());
            _isWaiting = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_timeToWait);
        _mainMenuManager.StartTransition();
        Debug.Log("Transition started");
    }
}
