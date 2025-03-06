using System;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] Animator _animationRenderer;
    [SerializeField] SpriteRenderer[] _spritesLifeRenderers;
    [SerializeField] Sprite[] _spritesLife;
    private MovementPlayer _movementPlayer;
    public event Action OnCorrupt;
    [SerializeField] UnityEvent _onCorrupt;
    private ReverseAction _reverseAction = new ReverseAction();

    private void Start()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.SoulDamage;

        _movementPlayer.OnCaseMouvEnd += HurtSelf;
        UpdateAnimator();
    }

    private void HurtSelf()
    {
        int newState = (int)_state + 1;
        _state = (State)(newState);

        if (_state == State.stateDie)
        {
            OnCorrupt?.Invoke();
            _onCorrupt?.Invoke();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBA");
            UpdateAnimator();
            GameManager.Instance.EndGame(true);
        }
        else
        {
            _onHurt?.Invoke();
            UpdateAnimator();
            SfxManager.Instance.PlaySound2D("SoulCorrupt");
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
        UpdateAnimator();
    }

    public void UpdateAnimator()
    {
        int nbState = (int)_state;
        _animationRenderer.SetInteger("Life",nbState);
        if(Application.isEditor && !Application.isPlaying)_animationRenderer.GetComponent<SpriteRenderer>().sprite = _spritesLife[nbState];
        int count = 0;
        foreach (SpriteRenderer spriteLifeRenderer in _spritesLifeRenderers)
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

    public int GetCurrentColor()
    {
        return (int)_state;
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
