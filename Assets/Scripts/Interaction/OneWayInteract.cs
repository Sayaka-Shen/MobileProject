using System;
using UnityEngine;

public class OneWayInteract : MonoBehaviour, IInteractable
{
    private MovementPlayer _movementPlayer;
    private enum Type { River, Ledge}
    [SerializeField] Type _type;
    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.PlayerMove;
        _reverseAction.PositionTarget = transform.position;
    }

    private void Start()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;
    }

    public void Interact()
    {
        RollbackManager.Instance.AddAction(_reverseAction);
        _movementPlayer.AddCaseMov(1);
        _movementPlayer.AddPos(transform.position + transform.right);
        _movementPlayer.StartMoving();
        
        _movementPlayer.CountMove = false;
        _movementPlayer.OnEndMove += RestartCountMove;
        if(_type == Type.Ledge) GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBQ");
        else if (_type == Type.River) GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBg");
    }

    private void RestartCountMove()
    {
        _movementPlayer.CountMove = true;
        _movementPlayer.RemoveCaseMov(1);
        _movementPlayer.OnEndMove -= RestartCountMove;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}
