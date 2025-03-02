using System;
using UnityEngine;

public class SoulsInteraction : MonoBehaviour, IInteractable
{
    [Header("Soul Settings")]
    [SerializeField] private GameObject _soulParent;
    private SoulPlayer _soulPlayer;
    private Seeker _seeker;

    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.SoulTake;
    }

    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
        _seeker = _soulParent.GetComponent<Seeker>();
    }

    public void Interact()
    {
        if (!_soulPlayer.AsSoul)
        {
            RollbackManager.Instance.AddAction(_reverseAction);
            _soulParent.GetComponent<Soul>().Desapere();
            if(_seeker != null) _seeker.UnFollow();
            _soulPlayer.TakeSoul();
        }
    }
}
