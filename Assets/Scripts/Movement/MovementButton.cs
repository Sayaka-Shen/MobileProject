using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour
{
    private MovementPlayer _mouvementPlayer;
    private bool _doShowButton = true;
    public bool DoShowButton { get => _doShowButton; set => _doShowButton = value; }

    private void Start()
    {
        _mouvementPlayer = GameManager.Instance.MovementPlayer;
        
        _mouvementPlayer.OnStartMove += Hide;
        if(_doShowButton)
        {
            _mouvementPlayer.OnEndMove += ShowCorrectButton;
            ShowCorrectButton();
        }
    }

    void OnMouseDown()
    {
        if(isPlaying(GameManager.Instance.AnimPlayer, "anim_idle") && Time.timeScale != 0)
        {
            Vector3 newPos = transform.position;
            newPos.z = 0;
            _mouvementPlayer.AddPos(newPos);
            _mouvementPlayer.StartMoving();
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            return true;
        else
            return false;
    }

    void ShowCorrectButton()
    {
        gameObject.SetActive(!CollisionManager.Instance.GetObstacleAt(transform.position));
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _mouvementPlayer.OnStartMove -= Hide;
        _mouvementPlayer.OnEndMove -= ShowCorrectButton;
    }
}
