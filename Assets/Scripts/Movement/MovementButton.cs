using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour
{
    private MovementPlayer _mouvementPlayer;
    private bool _doShowButton = true;
    public bool DoShowButton { get => _doShowButton; set => _doShowButton = value; }

    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.PlayerMove;
    }

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
        if(!IsPointerOverUIObject() && isPlaying(GameManager.Instance.AnimPlayer, "anim_idle") && Time.timeScale != 0)
        {
            Vector3 newPos = transform.position;
            newPos.z = 0;
            _reverseAction.PositionTarget = _mouvementPlayer.transform.position;
            RollbackManager.Instance.AddPlayerAction(_reverseAction);
            _mouvementPlayer.AddPos(newPos);
            _mouvementPlayer.StartMoving();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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
