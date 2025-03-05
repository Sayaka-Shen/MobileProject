using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Purify : MonoBehaviour, IInteractable
{
    [Header("Purify Settings")]
    [SerializeField] UnityEvent _onPurify;
    private SoulPlayer _soulPlayer;
    
    public static event Action OnPurify;
    private ReverseAction _reverseAction = new ReverseAction();
    [SerializeField] private Light2D _light;
    [SerializeField] private float _speed = .1f;

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.Purify;
        _light.intensity = 0f;
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
            StartCoroutine(Anim());
            _soulPlayer.DeleteSoul();
            SoulsManager.Instance.AddSoulsPurify();
            _onPurify?.Invoke();
            OnPurify?.Invoke();
            SfxManager.Instance.PlaySound2D("PurifySound");
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQAg");
        }
    }

    private IEnumerator Anim()
    {
        _soulPlayer.TogleUI();
        yield return new WaitForSeconds(1f);
        do
        {
            _light.intensity += _speed * Time.deltaTime;
        } while (_light.intensity <= 2f);

        yield return new WaitForSeconds(2f);
        do
        {
            _light.intensity -= _speed * Time.deltaTime;
        } while (_light.intensity >= 0f);
        _soulPlayer.TogleUI();

    }
}
