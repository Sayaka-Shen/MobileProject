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

    public void Setup()
    {
        _countMove = _countDownMove;

        _movementPlayer = GameManager.Instance.MovementPlayer;

        _movementPlayer.OnStop += Move;
    }

    public void UnFollow()
    {
        _movementPlayer = GameManager.Instance.MovementPlayer;
        _movementPlayer.OnStop -= Move;
    }

    private void Move()
    {
        _countMove--;

        if (_countMove == 0)
        {
            //A MODIFIER
            if (Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition) > 1 || !GameManager.Instance.SoulPlayer.AsSoul)
            {
                _reverseAction.PositionTarget = transform.position;
                RollbackManager.Instance.AddAction(_reverseAction);
                MoveTo(Pathfinding.Instance.FindPath(transform.position, GameManager.Instance.PlayerPosition));
            }
            if(Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition) == 0)
            {
                GetComponent<Soul>().Desapere();
                UnFollow();
                GameManager.Instance.SoulPlayer.TakeSoul();
            }
            _countMove = _countDownMove;
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }
}
