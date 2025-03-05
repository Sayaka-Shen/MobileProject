using UnityEngine;

public class Seeker : MonoBehaviour
{
    [Header("Seeker Settings")]
    [SerializeField] private int _countDownMove = 2;
    private int _countMove = 0;
    private MovementPlayer _movementPlayer;
    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.SeekerMove;
    }
    private void Start()
    {
        _countMove = _countDownMove;

        _movementPlayer = GameManager.Instance.MovementPlayer;

        _movementPlayer.OnStop += Move;

        GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCA");
    }

    private void Move()
    {
        if(!GameManager.Instance.MovementPlayer.CountMove) return;
        _reverseAction.ValueTarget = _countMove;
        RollbackManager.Instance.AddAction(_reverseAction);
        _reverseAction.ValueTarget = 0;
        _countMove--;

        if (_countMove == 0)
        {
            if (Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition) != 0)
            {
                _reverseAction.PositionTarget = transform.position;
                RollbackManager.Instance.AddAction(_reverseAction);

                Vector3 destination = Pathfinding.Instance.FindPath(transform.position, GameManager.Instance.PlayerPosition);

                if(destination == transform.position)
                {
                    // MoveTo(Pathfinding.Instance.GetRandomNeighbor(transform.position));
                }
                else
                {
                    MoveTo(destination);
                }
            }

            if(Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition) == 0)
            {
                GameManager.Instance.EndGame(true);
            }
            _countMove = _countDownMove;
        }
    }

    public void RerollMove(int value)
    {
        _countMove = value;
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }
}
