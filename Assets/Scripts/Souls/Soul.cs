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
    private ReverseAction _reverseAction = new ReverseAction();

    void Start()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.SoulDamage;

        _movementPlayer.OnCaseMouvEnd += HurtSelf;
        UpdateSprite();
        _seeker = GetComponent<Seeker>();
    }

    void HurtSelf()
    {
        int newState = (int)_state + 1;
        _state = (State)(newState);
        if(_seeker != null && _state == State.stateThree)
        {
            Desapere();
            _seeker.Setup();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCA");
        }
        if (_state == State.stateDie)
        {
            OnCorrupt?.Invoke();
            _onCorrupt?.Invoke();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBA");
            gameObject.SetActive(false);
        }
        else
        {
            _onHurt?.Invoke();
            UpdateSprite();
        }
        RollbackManager.Instance.AddAction(_reverseAction);
    }

    public void Heal()
    {
        if (_state == State.stateDie)
        {
            gameObject.SetActive(true);
            _movementPlayer.OnCaseMouvEnd += HurtSelf;
            SoulsManager.Instance.RemoveSoulsCorrupt();
        }
        int newState = (int)_state - 1;
        _state = (State)(newState);
        UpdateSprite();
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

    public void Desapere()
    {
        gameObject.SetActive(false);
        _movementPlayer.OnCaseMouvEnd -= HurtSelf;
    }

    public void Reapere()
    {
        gameObject.SetActive(true);
        _movementPlayer.OnCaseMouvEnd += HurtSelf;
    }

    private void OnDestroy()
    {
        _movementPlayer.OnCaseMouvEnd -= HurtSelf;
    }
}
