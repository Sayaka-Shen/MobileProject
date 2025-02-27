using UnityEngine;

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
        _mouvementPlayer.AddPos(transform.position);
        _mouvementPlayer.StartMoving();
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
