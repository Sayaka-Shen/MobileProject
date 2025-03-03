using System;
using UnityEngine;

public class SoulsInteraction : MonoBehaviour, IInteractable
{
    [Header("Soul Settings")]
    [SerializeField] private GameObject _soulParent;
    private SoulPlayer _soulPlayer;

    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = _soulParent;
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
            RollbackManager.Instance.AddAction(_reverseAction);
            _soulParent.GetComponent<Soul>().Desapere();
            _soulPlayer.TakeSoul();
        }
    }
}
