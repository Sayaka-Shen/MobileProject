using UnityEngine;

public class AddMovPower : MonoBehaviour, IInteractable
{
    private MovementPlayer _movementPlayer;

    [SerializeField] private GameObject _parent;
    [SerializeField] int _nbCasePower = 2;

    private void Start()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;
    }

    public void Interact()
    {
        _movementPlayer.AddCaseMov(_nbCasePower);
        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQBw");
        Destroy(_parent);
    }
}
