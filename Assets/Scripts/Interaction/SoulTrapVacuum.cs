using UnityEngine;

public class SoulTrapVacuum : MonoBehaviour, IInteractable
{
    private SoulPlayer _soulPlayer;

    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.SoulTaken;
    }

    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
    }

    public void Interact()
    {
        if(_soulPlayer.AsSoul)
        {
            RollbackManager.Instance.AddAction(_reverseAction);
            _soulPlayer.DeleteSoul();
            SoulsManager.Instance.AddSoulsCorrupt();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCQ");
        }
    }
}
