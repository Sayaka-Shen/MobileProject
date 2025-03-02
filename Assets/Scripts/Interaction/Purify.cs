using System;
using UnityEngine;
using UnityEngine.Events;

public class Purify : MonoBehaviour, IInteractable
{
    [Header("Purify Settings")]
    [SerializeField] UnityEvent _onPurify;
    private SoulPlayer _soulPlayer;
    
    public static event Action OnPurify;
    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.Purify;
    }

    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
    }

    public void Interact()
    {
        if (_soulPlayer.AsSoul)
        {
            RollbackManager.Instance.AddAction(_reverseAction);
            GameManager.Instance.AnimPlayer.SetTrigger("purification");
            _soulPlayer.DeleteSoul();
            SoulsManager.Instance.AddSoulsPurify();
            _onPurify?.Invoke();
            OnPurify?.Invoke();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQAg");
        }
    }
}
