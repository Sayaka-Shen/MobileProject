using UnityEngine;
using UnityEngine.Video;

public class TransitionVideo : MonoBehaviour
{
    [SerializeField] private MainMenuManager _mainMenuManager;
    [SerializeField] private VideoPlayer _videoPlayer;

        void Start()
    {
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer _videoPlayer)
    {
        _mainMenuManager.ReturnSelector();
    }
}
