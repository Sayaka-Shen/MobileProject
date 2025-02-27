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

    public void UnFollow()
    {
        _movementPlayer.OnEndMove -= Move;
    }

    private void Move()
    {
        _countMove--;

        if (_countMove == 0)
        {
            if (Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition) > 1 || !GameManager.Instance.SoulPlayer.AsSoul)
            {
                transform.position = Pathfinding.Instance.FindPath(transform.position, GameManager.Instance.PlayerPosition);
            }
            if(Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition) == 0)
            {
                GetComponent<Soul>().UnFollow();
                UnFollow();
                Destroy(gameObject);
                GameManager.Instance.SoulPlayer.TakeSoul();
            }
            _countMove = _countDownMove;
        }
    }
}
