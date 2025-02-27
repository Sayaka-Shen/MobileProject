using System;
using UnityEngine;

public class SoulsInteraction : MonoBehaviour, IInteractable
{
    [Header("Soul Settings")]
    [SerializeField] private GameObject _soulParent;
    private SoulPlayer _soulPlayer;
    
    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
    }

    public void Interact()
    {
        if (!_soulPlayer.AsSoul)
        {
            _soulParent.GetComponent<Soul>().UnFollow();
            Destroy(_soulParent.gameObject);
            _soulPlayer.TakeSoul();
        }
    }
}
