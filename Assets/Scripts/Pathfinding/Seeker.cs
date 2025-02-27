using UnityEngine;

public class Seeker : MonoBehaviour
{
    [Header("Seeker Settings")]
    [SerializeField] private int _countDownMove = 2;
    private int _countMove = 0;
    private MovementPlayer _movementPlayer;

    public void Setup()
    {
        _countMove = _countDownMove;

        _movementPlayer = GameManager.Instance.MovementPlayer;

        _movementPlayer.OnEndMove += Move;
    }

    private void Move()
    {
        _countMove--;

        if (_countMove == 0)
        {
            transform.position = Pathfinding.Instance.FindPath(transform.position, GameManager.Instance.PlayerPosition);
            _countMove = _countDownMove;
        }
    }
}
