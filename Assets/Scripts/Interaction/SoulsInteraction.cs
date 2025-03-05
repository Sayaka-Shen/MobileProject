using System;
using UnityEngine;

public class SoulsInteraction : MonoBehaviour, IInteractable
{
    [Header("Soul Settings")]
    [SerializeField] private Soul _soulParent;
    private SoulPlayer _soulPlayer;

    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = _soulParent.gameObject;
        _reverseAction.Type = ReverseActionType.SoulTake;
    }

    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
    }

    public void Interact()
    {
        if (!_soulPlayer.AsSoul)
        {
            _reverseAction.ValueTarget = _soulParent.GetCurrentColor();
            RollbackManager.Instance.AddAction(_reverseAction);
            _soulParent.Desapere();
            _soulPlayer.TakeSoul(_reverseAction.ValueTarget);
        }
    }
}
