using UnityEngine;

public class AddMovPower : MonoBehaviour, IInteractable
{
    private MovementPlayer _movementPlayer;

    [SerializeField] private GameObject _parent;
    [SerializeField] int _nbCasePower = 2;
    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = _parent;
        _reverseAction.Type = ReverseActionType.MorePower;
    }

    private void Start()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;
    }

    public void Interact()
    {
        RollbackManager.Instance.AddAction(_reverseAction);
        _movementPlayer.AddCaseMov(_nbCasePower);
        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBw");
        _parent.SetActive(false);
    }
}
