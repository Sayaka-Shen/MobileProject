using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public class Soul : MonoBehaviour
{
    public enum State
    {
        stateOne, stateTwo, stateThree, stateDie
    }

    [Header("State")]
    [SerializeField] State _state = State.stateOne;
    [SerializeField] UnityEvent _onHurt;
    [Header("Visual")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] SpriteRenderer[] _spriteLifeRenderers;
    [SerializeField] Sprite[] _spritesLife;
    private MovementPlayer _movementPlayer;
    public event Action OnCorrupt;
    [SerializeField] UnityEvent _onCorrupt;
    private Seeker _seeker;

    void Start()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;

        _movementPlayer.OnStepEnd += HurtSelf;
        UpdateSprite();
    }

    void HurtSelf()
    {
        int newState = (int)_state + 1;
        _state = (State)(newState);
        if(TryGetComponent<Seeker>(out _seeker) && _state == State.stateThree)
        {
            UnFollow();
            _seeker.Setup();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCA");
        }
        if (_state == State.stateDie)
        {
            OnCorrupt?.Invoke();
            _onCorrupt?.Invoke();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBA");
            Destroy(gameObject);
        }
        else
        {
            _onHurt?.Invoke();
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        int nbState = (int)_state;
        _spriteRenderer.sprite = _sprites[nbState];
        int count = 0;
        foreach (SpriteRenderer spriteLifeRenderer in _spriteLifeRenderers)
        {
            if (count <= 2 - nbState)
            {
                spriteLifeRenderer.gameObject.SetActive(true);
                spriteLifeRenderer.sprite = _spritesLife[nbState];
            }
            else
            {
                spriteLifeRenderer.gameObject.SetActive(false);
            }
            count++;
        }
    }

    public void UnFollow() 
    {
        _movementPlayer.OnStepEnd -= HurtSelf;
    }

    private void OnDestroy()
    {
        UnFollow();
    }
}
