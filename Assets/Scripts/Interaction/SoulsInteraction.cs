using System;
using UnityEngine;

public class SoulsInteraction : MonoBehaviour, IInteractable
{
    [Header("Soul Settings")]
    [SerializeField] private GameObject _soulParent;
    private SoulPlayer _soulPlayer;
    private Seeker _seeker;
    
    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
        _seeker = _soulParent.GetComponent<Seeker>();
    }

    public void Interact()
    {
        if (!_soulPlayer.AsSoul)
        {
            _soulParent.GetComponent<Soul>().UnFollow();
            if(_seeker != null) _seeker.UnFollow();
            Destroy(_soulParent.gameObject);
            _soulPlayer.TakeSoul();
        }
    }
}
