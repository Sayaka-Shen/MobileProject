using System;
using UnityEngine;
using UnityEngine.Events;

public class Purify : MonoBehaviour, IInteractable
{
    [Header("Purify Settings")]
    [SerializeField] UnityEvent _onPurify;
    private SoulPlayer _soulPlayer;
    
    public static event Action OnPurify;
    
    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
    }

    public void Interact()
    {
        if (_soulPlayer.AsSoul)
        {
            GameManager.Instance.AnimPlayer.SetTrigger("purification");
            _soulPlayer.DeleteSoul();
            SoulsManager.Instance.AddSoulsPurify();
            _onPurify?.Invoke();
            OnPurify?.Invoke();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQAg");
        }
    }
}
